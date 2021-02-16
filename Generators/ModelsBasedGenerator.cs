using CodeGen.Models;
using CodeGen.Services;
using CodeGen.Templates;
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
            foreach (CodeGenModel model in _configService.CodeGenConfig.Models.List)
            {
                // File path
                string filePath = Path.Combine(
                    _configService.CodeGenConfig.Paths.Output,
                    Path.GetDirectoryName(projectTemplateFile),
                    string.Format(data.Output, model.Name, model.NamePlural)
                );
                filePath = filePath.Replace("Templates\\", "");

                // File text
                string templateTypeFormat = projectTemplateFile.Replace("\\", ".").Replace(".tt", "");
                Type templateType = Type.GetType($"CodeGen.{templateTypeFormat}, CodeGen");
                if (templateType == null) {
                    throw new Exception($"Can't get type for T4 template: CodeGen.{templateTypeFormat}, CodeGen");
                }
                ITextTemplate template = Activator.CreateInstance(templateType, _configService.CodeGenConfig, model) as ITextTemplate;
                string fileText = template.TransformText();

                _fileService.Create(filePath, fileText);
            }

            return Task.CompletedTask;
        }
    }
}
