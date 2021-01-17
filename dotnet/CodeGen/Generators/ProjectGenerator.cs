using CodeGen.Models;
using CodeGen.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
        private readonly CodeGenConfig _codeGenConfig;
        private readonly IFileService _fileService;
        private readonly IConfigBasedGenerator _configBasedGenerator;
        private readonly IModelsBasedGenerator _modelsBasedGenerator;

        public ProjectGenerator(
            ILogger<ProjectGenerator> logger,
            IOptions<CodeGenConfig> codeGenConfigOptions,
            IFileService fileService,
            IConfigBasedGenerator configBasedGenerator,
            IModelsBasedGenerator modelsBasedGenerator
        )
        {
            _logger = logger;
            _codeGenConfig = codeGenConfigOptions.Value;
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
                        }
                    }
                }

                // Template settings not found? Next project..
                if (codeGenTemplateSettings == null) { continue; }

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
            }
        }

        private Task CleanupOutputDirectory()
        {
            return _fileService.DeleteDirectory(_codeGenConfig.Paths.Output);
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
