using CodeGen.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.Services
{
    public interface IProjectRunnerService
    {
        Task Run();
    }

    public class ProjectRunnerService : IProjectRunnerService
    {
        private readonly ILogger<ProjectRunnerService> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;

        public ProjectRunnerService(
            ILogger<ProjectRunnerService> logger,
            IConfigService configService,
            IFileService fileService
        )
        {
            _logger = logger;
            _configService = configService;
            _fileService = fileService;
        }

        public async Task Run()
        {
            string projectTemplateName = _configService.CodeGenConfig.TemplateName;

            _logger.LogInformation("Running project: " + projectTemplateName);

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

            // Start project
            ProcessStartInfo dotnetRun = new ProcessStartInfo("dotnet");
            // TODO: Configurable --urls param
            //dotnetRun.Arguments = @"run";
            dotnetRun.Arguments = @"run --urls http://0.0.0.0:5001";
            dotnetRun.WorkingDirectory = startupProjectPath;
            await Process.Start(dotnetRun).WaitForExitAsync();
        }
    }
}
