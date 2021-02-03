using CodeGen.Models;
using CodeGen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeGen.Generators
{
    public interface IConfigBasedGenerator
    {
        Task Generate(string projectTemplateFile, CodeGenTemplateSettingsData data);
    }

    public class ConfigBasedGenerator : IConfigBasedGenerator
    {
        private readonly ILogger<ConfigBasedGenerator> _logger;
        private readonly IAppSettingsService _appSettingsService;
        private readonly IFileService _fileService;

        public ConfigBasedGenerator(
            ILogger<ConfigBasedGenerator> logger,
            IAppSettingsService appSettingsService,
            IFileService fileService
        )
        {
            _logger = logger;
            _appSettingsService = appSettingsService;
            _fileService = fileService;
        }

        public Task Generate(string projectTemplateFile, CodeGenTemplateSettingsData data)
        {
            // File path
            string filePath = Path.Combine(
                _appSettingsService.CodeGenConfig.Paths.Output,
                Path.GetDirectoryName(projectTemplateFile),
                data.Output
            );
            filePath = filePath.Replace("Templates\\", "");

            // File text
            string templateTypeFormat = projectTemplateFile.Replace("\\", ".").Replace(".tt", "");
            Type templateType = Type.GetType($"CodeGen.{templateTypeFormat}, CodeGen");
            if (templateType == null) {
                throw new Exception($"Can't get type for T4 template: CodeGen.{templateTypeFormat}, CodeGen");
            }

            var template = Activator.CreateInstance(templateType, _appSettingsService.CodeGenConfig) as dynamic;
            string fileText = template.TransformText();

            _fileService.Create(filePath, fileText);

            return Task.CompletedTask;
        }
    }
}
