using CodeGen.Models;
using CodeGen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
        private readonly IAppSettingsService _appSettingsService;
        private readonly IFileService _fileService;
        private readonly IConfigBasedGenerator _configBasedGenerator;
        private readonly IModelsBasedGenerator _modelsBasedGenerator;

        public ProjectGenerator(
            ILogger<ProjectGenerator> logger,
            IAppSettingsService appSettingsService,
            IFileService fileService,
            IConfigBasedGenerator configBasedGenerator,
            IModelsBasedGenerator modelsBasedGenerator
        )
        {
            _logger = logger;
            _appSettingsService = appSettingsService;
            _fileService = fileService;
            _configBasedGenerator = configBasedGenerator;
            _modelsBasedGenerator = modelsBasedGenerator;
        }

        public async Task Generate()
        {
            await CleanupOutputDirectory();

            _logger.LogInformation("Generating projects");

            List<string> projectTemplates = await ListProjectTemplates();
            foreach (string projectTemplate in projectTemplates)
            {
                _logger.LogInformation($"Project template: {projectTemplate}");

                // List files within the project templates directory
                List<string> projectTemplateFiles = await _fileService.TraverseDirectory(Path.Combine("Templates", "Projects", projectTemplate));

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
                        _appSettingsService.CodeGenConfig.Paths.Output,
                        Path.GetDirectoryName(projectTemplateFile),
                        projectTemplateFileName
                    );
                    filePath = filePath.Replace("Templates\\", "");

                    // File text
                    string fileText = await _fileService.Read(projectTemplateFile);

                    await _fileService.Create(filePath, fileText);
                }

                // Search for template settings
                CodeGenTemplateSettings codeGenTemplateSettings = null;
                foreach (string projectTemplateFile in projectTemplateFiles)
                {
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

                // Template settings not found? Next project..
                if (codeGenTemplateSettings == null) { continue; }

                projectTemplateFiles.Sort();

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

                // Determine startup project path
                string startupProjectPath = Path.Combine(
                    _appSettingsService.CodeGenConfig.Paths.Output,
                    codeGenTemplateSettings.TemplatePath,
                    codeGenTemplateSettings.StartupProjectPath
                );
                startupProjectPath = startupProjectPath.Replace("Templates\\", "");

                // ApplicationDbContext
                if (codeGenTemplateSettings.RecreateDatabaseAfterGenerate)
                {
                    // Drop the existing database
                    ProcessStartInfo dotnetDropDatabase = new ProcessStartInfo("dotnet");
                    dotnetDropDatabase.Arguments = @"ef database drop --force";
                    dotnetDropDatabase.WorkingDirectory = startupProjectPath;
                    Process.Start(dotnetDropDatabase).WaitForExit();

                    // Generate new Initial migration
                    ProcessStartInfo dotnetAddInitialMigration = new ProcessStartInfo("dotnet");
                    dotnetAddInitialMigration.Arguments = @"ef migrations add Initial --output-dir .\DAL\Migrations";
                    dotnetAddInitialMigration.WorkingDirectory = startupProjectPath;
                    Process.Start(dotnetAddInitialMigration).WaitForExit();
                }

                // Test project after generate
                if (codeGenTemplateSettings.TestProjectAfterGenerate)
                {
                    ProcessStartInfo dotnetRun = new ProcessStartInfo("dotnet");
                    dotnetRun.Arguments = @"run";
                    dotnetRun.WorkingDirectory = startupProjectPath;
                    Process.Start(dotnetRun).WaitForExit();
                }
            }
        }

        private Task CleanupOutputDirectory()
        {
            return _fileService.DeleteDirectory(_appSettingsService.CodeGenConfig.Paths.Output);
        }

        private async Task<List<string>> ListProjectTemplates()
        {
            List<string> projectTemplates = new List<string>();

            List<string> subDirectories = await _fileService.GetSubdirectories(Path.Combine("Templates", "Projects"));
            foreach (string directoryPath in subDirectories)
            {
                string[] directoryPathArr = directoryPath.Split("\\");
                string directory = directoryPathArr[directoryPathArr.Length - 1];

                if (!projectTemplates.Contains(directory))
                {
                    projectTemplates.Add(directory);
                }
            }

            return projectTemplates;
        }
    }
}
