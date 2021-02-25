using CodeGen.Models;
using CodeGen.Services;
using CodeGen.Templates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.Generators
{
    public interface IProjectGenerator
    {
        Task Generate();
    }

    public class ProjectGenerator : IProjectGenerator
    {
        private readonly ILogger<ProjectGenerator> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;

        protected string _projectTemplateName = null;
        protected List<string> _projectTemplateFiles = new List<string>();
        protected CodeGenTemplateSettings _projectTemplateSettings = null;
        protected string _outputProjectPath = null;

        public ProjectGenerator(
            ILogger<ProjectGenerator> logger,
            IConfigService configService,
            IFileService fileService
        )
        {
            _logger = logger;
            _configService = configService;
            _fileService = fileService;
        }

        public async Task Generate()
        {
            _projectTemplateName = _configService.AppSettings.GetValue<string>("Template:Name");

            _logger.LogInformation("Generating project from template: " + _projectTemplateName);

            // List files within the project templates directory
            _projectTemplateFiles = await _fileService.TraverseDirectory(Path.Combine("Templates", "Projects", _projectTemplateName));
            _projectTemplateFiles = _projectTemplateFiles.Select(x => x.Replace('\\', '/')).ToList();

            // Search for template settings
            foreach (string projectTemplateFile in _projectTemplateFiles)
            {
                if (_projectTemplateSettings != null) { continue; }
                if (projectTemplateFile.EndsWith("templatesettings.json"))
                {
                    string templateSettingsJson = await _fileService.Read(projectTemplateFile);
                    if (!string.IsNullOrEmpty(templateSettingsJson))
                    {
                        _projectTemplateSettings = JsonConvert.DeserializeObject<CodeGenTemplateSettings>(templateSettingsJson);
                        _projectTemplateSettings.TemplatePath = Path.GetDirectoryName(projectTemplateFile);
                    }
                }
            }

            // Template settings not found
            if (_projectTemplateSettings == null)
            {
                _logger.LogInformation($"Project template settings not found for: {_projectTemplateName}");
                return;
            }

            await CleanupProjectOutputDirectory(_projectTemplateName);

            // Sort files for better generation output
            _projectTemplateFiles.Sort();

            // Filter all template generation files
            List<string> templateGenerationFiles = new List<string>();
            foreach (string projectTemplateFile in _projectTemplateFiles)
            {
                string projectTemplateFileName = Path.GetFileName(projectTemplateFile);

                if (projectTemplateFileName.EndsWith("templatesettings.json") ||
                    projectTemplateFileName.EndsWith(".tt") ||
                    projectTemplateFileName.EndsWith(".Generated.cs"))
                {
                    templateGenerationFiles.Add(projectTemplateFile);
                }
            }

            // Also exclude .cs files from .tt files
            foreach (string projectTemplateFile in _projectTemplateFiles)
            {
                string projectTemplateFileName = Path.GetFileName(projectTemplateFile);

                if (projectTemplateFileName.EndsWith(".cs"))
                {
                    if (templateGenerationFiles.Any(x => x.Contains(Path.GetFileNameWithoutExtension(projectTemplateFileName) + ".tt")))
                    {
                        templateGenerationFiles.Add(projectTemplateFile);
                    }
                }
            }

            // Filter excluded files
            List<string> excludedFiles = new List<string>();
            foreach (string projectTemplateFile in _projectTemplateFiles)
            {
                string projectTemplateFileName = Path.GetFileName(projectTemplateFile);

                if (_projectTemplateSettings.Exclude.Any(x => projectTemplateFile.Contains(x)))
                {
                    excludedFiles.Add(projectTemplateFile);
                }
            }

            // Copy all non template generation files to output
            foreach (string projectTemplateFile in _projectTemplateFiles)
            {
                if (excludedFiles.Contains(projectTemplateFile) ||
                    templateGenerationFiles.Contains(projectTemplateFile)) { continue; }

                string projectTemplateFileName = Path.GetFileName(projectTemplateFile);

                // File path
                string filePath = Path.Combine(
                    "_Output",
                    Path.GetDirectoryName(projectTemplateFile),
                    projectTemplateFileName
                );
                filePath = filePath.Replace('\\', '/').Replace("Templates/", "");

                // Create output directory if not exists
                await _fileService.CreateDirectory(Path.GetDirectoryName(filePath));

                // Copy file to output directory if newer
                if (await _fileService.Exists(filePath))
                {
                    _logger.LogInformation($"Replace file if newer: " + filePath);
                    await _fileService.CopyIfNewer(projectTemplateFile, filePath);
                }
                else
                {
                    _logger.LogInformation($"Copy file to: " + filePath);
                    await _fileService.Copy(projectTemplateFile, filePath);
                }
            }

            // Generate files with config
            foreach (CodeGenTemplateSettingsData data in _projectTemplateSettings.ConfigBasedGenerator)
            {
                foreach (string projectTemplateFile in _projectTemplateFiles)
                {
                    if (projectTemplateFile.EndsWith(data.T4Template))
                    {
                        await GenerateBasedOnConfig(projectTemplateFile, data);
                    }
                }
            }

            // Generate files based on each model
            foreach (CodeGenTemplateSettingsData data in _projectTemplateSettings.ModelsBasedGenerator)
            {
                foreach (string projectTemplateFile in _projectTemplateFiles)
                {
                    if (projectTemplateFile.EndsWith(data.T4Template))
                    {
                        await GenerateForEachModel(projectTemplateFile, data);
                    }
                }
            }

            // Output project path
            _outputProjectPath = Path.Combine(
                "_Output",
                _projectTemplateSettings.TemplatePath
            );
            _outputProjectPath = _outputProjectPath.Replace('\\', '/').Replace("Templates/", "");
        }

        private async Task CleanupProjectOutputDirectory(string projectName)
        {
            string projectOutputFolderPath = Path.Combine("_Output", "Projects", projectName);
            projectOutputFolderPath = projectOutputFolderPath.Replace('\\', '/');

            if (!_fileService.DirectoryExists(projectOutputFolderPath)) { return; }

            _logger.LogInformation("Cleaning project output folder: " + projectOutputFolderPath);

            ProcessStartInfo gitAdd = new ProcessStartInfo("git");
            gitAdd.Arguments = $"add .";
            gitAdd.WorkingDirectory = projectOutputFolderPath;
            await Process.Start(gitAdd).WaitForExitAsync();

            ProcessStartInfo gitCheckoutDirectory = new ProcessStartInfo("git");
            gitCheckoutDirectory.Arguments = $"restore --source=HEAD --staged --worktree -- .";
            gitCheckoutDirectory.WorkingDirectory = projectOutputFolderPath;
            await Process.Start(gitCheckoutDirectory).WaitForExitAsync();
        }

        private Task GenerateBasedOnConfig(string projectTemplateFile, CodeGenTemplateSettingsData data)
        {
            // File path
            string filePath = Path.Combine(
                "_Output",
                Path.GetDirectoryName(projectTemplateFile),
                data.Output
            );
            filePath = filePath.Replace('\\', '/').Replace("Templates/", "");

            // File text
            string templateTypeFormat = projectTemplateFile.Replace("/", ".").Replace(".tt", "");
            Type templateType = Type.GetType($"CodeGen.{templateTypeFormat}, CodeGen");
            if (templateType == null)
            {
                throw new Exception($"Can't get type for T4 template: CodeGen.{templateTypeFormat}, CodeGen");
            }
            ITextTemplate template = Activator.CreateInstance(templateType, _configService.CodeGenConfig) as ITextTemplate;
            string fileText = template.TransformText();

            _logger.LogInformation("Create file: " + filePath);
            _fileService.Create(filePath, fileText);

            return Task.CompletedTask;
        }

        private Task GenerateForEachModel(string projectTemplateFile, CodeGenTemplateSettingsData data)
        {
            foreach (CodeGenModel model in _configService.CodeGenConfig.Models.List)
            {
                // File path
                string filePath = Path.Combine(
                    "_Output",
                    Path.GetDirectoryName(projectTemplateFile),
                    string.Format(data.Output, model.Name, model.NamePlural, model.Name.ToLower(), model.NamePlural.ToLower())
                );
                filePath = filePath.Replace('\\', '/').Replace("Templates/", "");

                // File text
                string templateTypeFormat = projectTemplateFile.Replace("/", ".").Replace(".tt", "");
                Type templateType = Type.GetType($"CodeGen.{templateTypeFormat}, CodeGen");
                if (templateType == null)
                {
                    throw new Exception($"Can't get type for T4 template: CodeGen.{templateTypeFormat}, CodeGen");
                }
                ITextTemplate template = Activator.CreateInstance(templateType, _configService.CodeGenConfig, model) as ITextTemplate;
                string fileText = template.TransformText();

                _logger.LogInformation("Create file: " + filePath);
                _fileService.Create(filePath, fileText);
            }

            return Task.CompletedTask;
        }

        // OLD

        //private async Task<List<string>> ListProjectTemplates()
        //{
        //    List<string> projectTemplates = new List<string>();

        //    List<string> subDirectories = await _fileService.GetSubdirectories(Path.Combine("Templates", "Projects"));
        //    foreach (string directoryPath in subDirectories)
        //    {
        //        string directoryName = Path.GetFileName(directoryPath);
        //        if (!projectTemplates.Contains(directoryName))
        //        {
        //            projectTemplates.Add(directoryName);
        //        }
        //    }

        //    return projectTemplates;
        //}

        //private async Task CommitProjectOutputDirectory(string projectName)
        //{
        //    string projectOutputFolderPath = Path.Combine("_Output", "Projects", projectName);

        //    bool pathExists = _fileService.DirectoryExists(projectOutputFolderPath);
        //    if (!pathExists) { return; }

        //    _logger.LogInformation("Saving changes of project output folder: " + projectOutputFolderPath);

        //    ProcessStartInfo gitAdd = new ProcessStartInfo("git");
        //    gitAdd.Arguments = "add .";
        //    gitAdd.WorkingDirectory = projectOutputFolderPath;
        //    await Process.Start(gitAdd).WaitForExitAsync();

        //    ProcessStartInfo gitCommit = new ProcessStartInfo("git");
        //    gitCommit.Arguments = $"commit -m \"Save before regenerating project template {projectName}\"";
        //    gitCommit.WorkingDirectory = projectOutputFolderPath;
        //    await Process.Start(gitCommit).WaitForExitAsync();
        //}
    }
}
