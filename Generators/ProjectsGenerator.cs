using CodeGen.Models;
using CodeGen.Services;
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
    public interface IProjectsGenerator
    {
        Task Generate();
    }

    public class ProjectsGenerator : IProjectsGenerator
    {
        private readonly ILogger<ProjectsGenerator> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;
        private readonly IConfigBasedGenerator _configBasedGenerator;
        private readonly IModelsBasedGenerator _modelsBasedGenerator;

        public ProjectsGenerator(
            ILogger<ProjectsGenerator> logger,
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
            _logger.LogInformation("Generating projects");

            List<string> projectTemplates = await ListProjectTemplates();
            foreach (string projectTemplate in projectTemplates)
            {
                // List files within the project templates directory
                List<string> projectTemplateFiles = await _fileService.TraverseDirectory(Path.Combine("Templates", "Projects", projectTemplate));

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

                // Template settings not found or Generate on false => Skip to next project
                if (codeGenTemplateSettings == null || !codeGenTemplateSettings.Generate) {
                    _logger.LogInformation($"Skipping project template: {projectTemplate}");
                    continue;
                }

                _logger.LogInformation($"Regenerating project template: {projectTemplate}");

                // Saving changes before cleanup
                if (codeGenTemplateSettings.BeforeGenerate.CommitProjectOutputDirectory) {
                    await CommitProjectOutputDirectory(projectTemplate);
                }

                await CleanupProjectOutputDirectory(projectTemplate);

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
                    filePath = filePath.Replace("Templates\\", "");

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

                // Generation done

                // Output project path
                string outputProjectPath = Path.Combine(
                    _configService.CodeGenConfig.Paths.Output,
                    codeGenTemplateSettings.TemplatePath
                );
                outputProjectPath = outputProjectPath.Replace("Templates\\", "");

                // Output startup project path
                string startupProjectPath = Path.Combine(
                    outputProjectPath,
                    codeGenTemplateSettings.StartupProjectPath
                );

                // Build after generate
                if (codeGenTemplateSettings.AfterGenerate.Build)
                {
                    _logger.LogInformation("Buiding project: " + startupProjectPath);

                    ProcessStartInfo dotnetBuild = new ProcessStartInfo("dotnet");
                    dotnetBuild.Arguments = @"build";
                    dotnetBuild.WorkingDirectory = startupProjectPath;
                    Process dotnetBuildProcess = Process.Start(dotnetBuild);
                    await dotnetBuildProcess.WaitForExitAsync();
                    if (dotnetBuildProcess.ExitCode == 1) {
                        throw new Exception("Project build failed. Stopping next after generate steps");
                    }
                }

                // Recreate database after generate
                if (codeGenTemplateSettings.AfterGenerate.RecreateDatabase)
                {
                    _logger.LogInformation("Recreating database/migrations: " + startupProjectPath);

                    // Drop the existing database
                    ProcessStartInfo dotnetDropDatabase = new ProcessStartInfo("dotnet");
                    dotnetDropDatabase.Arguments = @"ef database drop --force" + (codeGenTemplateSettings.AfterGenerate.Build ? " --no-build" : "");
                    dotnetDropDatabase.WorkingDirectory = startupProjectPath;
                    await Process.Start(dotnetDropDatabase).WaitForExitAsync();

                    // Generate new Initial migration
                    ProcessStartInfo dotnetAddInitialMigration = new ProcessStartInfo("dotnet");
                    dotnetAddInitialMigration.Arguments = @"ef migrations add Initial --output-dir " + codeGenTemplateSettings.MigrationsFolderPath + (codeGenTemplateSettings.AfterGenerate.Build ? " --no-build" : "");;
                    dotnetAddInitialMigration.WorkingDirectory = startupProjectPath;
                    await Process.Start(dotnetAddInitialMigration).WaitForExitAsync();
                }

                // Install project template with dotnet new after generate
                if (codeGenTemplateSettings.AfterGenerate.InstallProjectTemplate) {
                    _logger.LogInformation("Installing project template: " + outputProjectPath);

                    ProcessStartInfo dotnetInstallProject = new ProcessStartInfo("dotnet");
                    dotnetInstallProject.Arguments = @"new -i ./";
                    dotnetInstallProject.WorkingDirectory = outputProjectPath;
                    await Process.Start(dotnetInstallProject).WaitForExitAsync();
                }

                // Open swagger after generate
                if (codeGenTemplateSettings.AfterGenerate.OpenSwagger) {
                    _logger.LogInformation("Opening Swagger docs: " + outputProjectPath);

                    ProcessStartInfo openSwagger = new ProcessStartInfo("explorer");
                    openSwagger.Arguments = codeGenTemplateSettings.StartupProjectURL;
                    await Process.Start(openSwagger).WaitForExitAsync();
                }

                // Run project after generate
                if (codeGenTemplateSettings.AfterGenerate.Run)
                {
                    _logger.LogInformation("Test run project: " + startupProjectPath);

                    ProcessStartInfo dotnetRun = new ProcessStartInfo("dotnet");
                    dotnetRun.Arguments = @"run";
                    dotnetRun.WorkingDirectory = startupProjectPath;
                    await Process.Start(dotnetRun).WaitForExitAsync();
                }
            }
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
