using CodeGen.Models;
using CodeGen.Services;
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
        private readonly CodeGenConfig _codeGenConfig;
        private readonly IFileService _fileService;

        public ConfigBasedGenerator(
            ILogger<ConfigBasedGenerator> logger,
            IOptions<CodeGenConfig> codeGenConfigOptions,
            IFileService fileService
        )
        {
            _logger = logger;
            _codeGenConfig = codeGenConfigOptions.Value;
            _fileService = fileService;
        }

        public Task Generate(string projectTemplateFile, CodeGenTemplateSettingsData data)
        {
            // File path
            string filePath = Path.Combine(
                _codeGenConfig.Paths.Output,
                Path.GetDirectoryName(projectTemplateFile),
                data.Output
            );
            filePath = filePath.Replace("Templates\\", "");

            // File text
            string templateTypeFormat = projectTemplateFile.Replace("\\", ".").Replace(".tt", "");
            Type templateType = Type.GetType($"CodeGen.{templateTypeFormat}, CodeGen");
            var template = Activator.CreateInstance(templateType, _codeGenConfig) as dynamic;
            string fileText = template.TransformText();

            _fileService.Create(filePath, fileText);

            return Task.CompletedTask;
        }
    }
}
