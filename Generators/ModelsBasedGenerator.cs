using CodeGen.Models;
using CodeGen.Services;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CodeGen.Generators
{
    public interface IModelsBasedGenerator
    {
        Task Generate(string projectTemplateFile, CodeGenTemplateSettingsData data);
    }

    public class ModelsBasedGenerator : IModelsBasedGenerator
    {
        private readonly ILogger<ModelsBasedGenerator> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;

        public ModelsBasedGenerator(
            ILogger<ModelsBasedGenerator> logger,
            IConfigService configService,
            IFileService fileService
        )
        {
            _logger = logger;
            _configService = configService;
            _fileService = fileService;
        }

        public Task Generate(string projectTemplateFile, CodeGenTemplateSettingsData data)
        {
            foreach (var model in _configService.CodeGenConfig.Models)
            {
                // File path
                string filePath = Path.Combine(
                    _configService.CodeGenConfig.Paths.Output,
                    Path.GetDirectoryName(projectTemplateFile),
                    string.Format(data.Output, model.Name)
                );
                filePath = filePath.Replace("Templates\\", "");

                // File text
                string templateTypeFormat = projectTemplateFile.Replace("\\", ".").Replace(".tt", "");
                Type templateType = Type.GetType($"CodeGen.{templateTypeFormat}, CodeGen");
                var template = Activator.CreateInstance(templateType, _configService.CodeGenConfig, model) as dynamic;
                string fileText = template.TransformText();

                _fileService.Create(filePath, fileText);
            }

            return Task.CompletedTask;
        }
    }
}
