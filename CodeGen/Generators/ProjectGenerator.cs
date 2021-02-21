using CodeGen.Models;
using CodeGen.Services;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
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
        private readonly IConfigBasedGenerator _configBasedGenerator;
        private readonly IModelsBasedGenerator _modelsBasedGenerator;

        public ProjectGenerator(
            ILogger<ProjectGenerator> logger,
            IConfigService configService,
            IFileService fileService,
            IConfigBasedGenerator configBasedGenerator,
            IModelsBasedGenerator modelsBasedGenerator
        )
        {
            _logger = logger;
            _configService = configService;
            _fileService = fileService;
            _configBasedGenerator = configBasedGenerator;
            _modelsBasedGenerator = modelsBasedGenerator;
        }

        public async Task Generate()
        {
            //List<string> projectTemplates = await ListProjectTemplates();
            //foreach (string projectTemplateName in projectTemplates)
            //{

            string projectTemplateName = _configService.CodeGenConfig.TemplateName;

            _logger.LogInformation("Generating project from template: " + projectTemplateName);

            // List files within the project templates directory
            List<string> projectTemplateFiles = await _fileService.TraverseDirectory(Path.Combine("Templates", "Projects", projectTemplateName));
            projectTemplateFiles = projectTemplateFiles.Select(x => x.Replace('\\', '/')).ToList();

            // Search for template settings
            CodeGenTemplateSettings codeGenTemplateSettings = null;
            foreach (string projectTemplateFile in projectTemplateFiles)
            {
                if (codeGenTemplateSettings != null) { continue; }
                if (projectTemplateFile.EndsWith("templatesettings.json"))
                {
                    string templateSettingsJson = await _fileService.Read(projectTemplateFile);
                    if (!string.IsNullOrEmpty(templateSettingsJson))
                    {
                        codeGenTemplateSettings = JsonConvert.DeserializeObject<CodeGenTemplateSettings>(templateSettingsJson);
                        codeGenTemplateSettings.TemplatePath = Path.GetDirectoryName(projectTemplateFile);
                    }
                }
            }

            // Template settings not found
            if (codeGenTemplateSettings == null)
            {
                _logger.LogInformation($"Project template settings not found for: {projectTemplateName}");
                return;
            }

            ////Generate on false => Skip to next project
            //if (!codeGenTemplateSettings.Generate)
            //{
            //    _logger.LogInformation($"Skipping project template: {projectTemplateName}");
            //    continue;
            //}

            //_logger.LogInformation($"Regenerating project template: {projectTemplateName}");

            //// Saving changes before cleanup
            //if (codeGenTemplateSettings.BeforeGenerate.CommitProjectOutputDirectory)
            //{
            //    await CommitProjectOutputDirectory(projectTemplateName);
            //}

            await CleanupProjectOutputDirectory(projectTemplateName);

            // Sort files for better generation output
            projectTemplateFiles.Sort();

            // Filter all template generation files
            List<string> templateGenerationFiles = new List<string>();
            foreach (string projectTemplateFile in projectTemplateFiles)
            {
                string projectTemplateFileName = Path.GetFileName(projectTemplateFile);

                if (projectTemplateFile.EndsWith("templatesettings.json") ||
                    projectTemplateFile.EndsWith(".tt") ||
                    projectTemplateFile.EndsWith(".Generated.cs"))
                {
                    templateGenerationFiles.Add(projectTemplateFile);
                }
            }

            // Also exclude .cs files from .tt files
            foreach (string projectTemplateFile in projectTemplateFiles)
            {
                string projectTemplateFileName = Path.GetFileName(projectTemplateFile);

                if (projectTemplateFile.EndsWith(".cs"))
                {
                    if (templateGenerationFiles.Any(x => x.Contains(Path.GetFileNameWithoutExtension(projectTemplateFileName) + ".tt")))
                    {
                        templateGenerationFiles.Add(projectTemplateFile);
                    }
                }
            }

            // Copy all non template generation files to output
            foreach (string projectTemplateFile in projectTemplateFiles)
            {
                if (templateGenerationFiles.Contains(projectTemplateFile)) { continue; }

                string projectTemplateFileName = Path.GetFileName(projectTemplateFile);

                // File path
                string filePath = Path.Combine(
                    _configService.CodeGenConfig.Paths.Output,
                    Path.GetDirectoryName(projectTemplateFile),
                    projectTemplateFileName
                );
                filePath = filePath.Replace('\\', '/').Replace("Templates/", "");

                // File text
                string fileText = await _fileService.Read(projectTemplateFile);

                await _fileService.Create(filePath, fileText);
            }

            // Generate files with config
            foreach (CodeGenTemplateSettingsData data in codeGenTemplateSettings.ConfigBasedGenerator)
            {
                foreach (string projectTemplateFile in projectTemplateFiles)
                {
                    if (projectTemplateFile.EndsWith(data.T4Template))
                    {
                        await _configBasedGenerator.Generate(projectTemplateFile, data);
                    }
                }
            }

            // Generate files based on each model
            foreach (CodeGenTemplateSettingsData data in codeGenTemplateSettings.ModelsBasedGenerator)
            {
                foreach (string projectTemplateFile in projectTemplateFiles)
                {
                    if (projectTemplateFile.EndsWith(data.T4Template))
                    {
                        await _modelsBasedGenerator.Generate(projectTemplateFile, data);
                    }
                }
            }

            // Output project path
            string outputProjectPath = Path.Combine(
                _configService.CodeGenConfig.Paths.Output,
                codeGenTemplateSettings.TemplatePath
            );
            outputProjectPath = outputProjectPath.Replace('\\', '/').Replace("Templates/", "");

            // Output startup project path
            string startupProjectPath = Path.Combine(
                outputProjectPath,
                codeGenTemplateSettings.StartupProjectPath
            );
            startupProjectPath = startupProjectPath.Replace('\\', '/');

            // Build after generate
            //if (codeGenTemplateSettings.AfterGenerate.Build)
            //{
            _logger.LogInformation("Buiding project: " + startupProjectPath);

            ProcessStartInfo dotnetBuild = new ProcessStartInfo("dotnet");
            dotnetBuild.Arguments = @"build";
            dotnetBuild.WorkingDirectory = startupProjectPath;
            Process dotnetBuildProcess = Process.Start(dotnetBuild);
            await dotnetBuildProcess.WaitForExitAsync();
            if (dotnetBuildProcess.ExitCode == 1)
            {
                throw new Exception("Project build failed. Stopping next after generate steps");
            }
            //}

            // Recreate database after generate
            //if (codeGenTemplateSettings.AfterGenerate.RecreateDatabase)
            //{
            _logger.LogInformation("Recreating database/migrations: " + startupProjectPath);

            // Drop the existing database
            ProcessStartInfo dotnetDropDatabase = new ProcessStartInfo("dotnet");
            dotnetDropDatabase.Arguments = @"ef database drop --force" + (codeGenTemplateSettings.AfterGenerate.Build ? " --no-build" : "");
            dotnetDropDatabase.WorkingDirectory = startupProjectPath;
            await Process.Start(dotnetDropDatabase).WaitForExitAsync();

            // Generate new Initial migration
            ProcessStartInfo dotnetAddInitialMigration = new ProcessStartInfo("dotnet");
            dotnetAddInitialMigration.Arguments = @"ef migrations add Initial --output-dir " + codeGenTemplateSettings.MigrationsFolderPath + (codeGenTemplateSettings.AfterGenerate.Build ? " --no-build" : ""); ;
            dotnetAddInitialMigration.WorkingDirectory = startupProjectPath;
            await Process.Start(dotnetAddInitialMigration).WaitForExitAsync();
            //}

            //// Install project template with dotnet new after generate
            //if (codeGenTemplateSettings.AfterGenerate.InstallProjectTemplate)
            //{
            //    _logger.LogInformation("Installing project template: " + outputProjectPath);

            //    ProcessStartInfo dotnetInstallProject = new ProcessStartInfo("dotnet");
            //    dotnetInstallProject.Arguments = @"new -i ./";
            //    dotnetInstallProject.WorkingDirectory = outputProjectPath;
            //    await Process.Start(dotnetInstallProject).WaitForExitAsync();
            //}

            //// Open swagger after generate
            //if (codeGenTemplateSettings.AfterGenerate.OpenSwagger)
            //{
            //    _logger.LogInformation("Opening Swagger docs: " + outputProjectPath);

            //    ProcessStartInfo openSwagger = new ProcessStartInfo("explorer");
            //    openSwagger.Arguments = codeGenTemplateSettings.StartupProjectURL;
            //    await Process.Start(openSwagger).WaitForExitAsync();
            //}
        }

        private async Task CommitProjectOutputDirectory(string projectName)
        {
            string projectOutputFolderPath = Path.Combine(_configService.CodeGenConfig.Paths.Output, "Projects", projectName);

            bool pathExists = _fileService.DirectoryExists(projectOutputFolderPath);
            if (!pathExists) { return; }

            _logger.LogInformation("Saving changes of project output folder: " + projectOutputFolderPath);

            ProcessStartInfo gitAdd = new ProcessStartInfo("git");
            gitAdd.Arguments = "add .";
            gitAdd.WorkingDirectory = projectOutputFolderPath;
            await Process.Start(gitAdd).WaitForExitAsync();

            ProcessStartInfo gitCommit = new ProcessStartInfo("git");
            gitCommit.Arguments = $"commit -m \"Save before regenerating project template {projectName}\"";
            gitCommit.WorkingDirectory = projectOutputFolderPath;
            await Process.Start(gitCommit).WaitForExitAsync();
        }

        private Task CleanupProjectOutputDirectory(string projectName)
        {
            string projectOutputFolderPath = Path.Combine(_configService.CodeGenConfig.Paths.Output, "Projects", projectName);

            _logger.LogInformation("Cleaning project output folder: " + projectOutputFolderPath);

            return _fileService.DeleteDirectory(projectOutputFolderPath);
        }

        private async Task<List<string>> ListProjectTemplates()
        {
            List<string> projectTemplates = new List<string>();

            List<string> subDirectories = await _fileService.GetSubdirectories(Path.Combine("Templates", "Projects"));
            foreach (string directoryPath in subDirectories)
            {
                string directoryName = Path.GetFileName(directoryPath);
                if (!projectTemplates.Contains(directoryName))
                {
                    projectTemplates.Add(directoryName);
                }
            }

            return projectTemplates;
        }
    }
}
