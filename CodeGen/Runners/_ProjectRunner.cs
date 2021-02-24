using CodeGen.Models;
using CodeGen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGen.Runners
{
    public interface IProjectRunner
    {
        Task Run();
    }

    public class ProjectRunner : IProjectRunner
    {
        private readonly ILogger<ProjectRunner> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;

        protected string _projectTemplateName = null;
        protected List<string> _projectTemplateFiles = new List<string>();
        protected CodeGenTemplateSettings _projectTemplateSettings = null;
        protected string _outputProjectPath = null;

        public ProjectRunner(
            ILogger<ProjectRunner> logger,
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
            _projectTemplateName = _configService.AppSettings.GetValue<string>("Template:Name");

            _logger.LogInformation("Running project: " + _projectTemplateName);

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

            // Output project path
            _outputProjectPath = Path.Combine(
                "_Output",
                _projectTemplateSettings.TemplatePath
            );
            _outputProjectPath = _outputProjectPath.Replace('\\', '/').Replace("Templates/", "");
        }
    }
}
