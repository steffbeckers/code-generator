using CodeGen.Models;
using CodeGen.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly CodeGenConfig _codeGenConfig;
        private readonly IFileService _fileService;

        public ModelsBasedGenerator(
            ILogger<ModelsBasedGenerator> logger,
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
            foreach (var model in _codeGenConfig.Models)
            {
                // File path
                string filePath = Path.Combine(
                    _codeGenConfig.Paths.Output,
                    Path.GetDirectoryName(projectTemplateFile),
                    string.Format(data.Output, model.Name)
                );
                filePath = filePath.Replace("Templates\\", "");

                // File text
                string templateTypeFormat = projectTemplateFile.Replace("\\", ".").Replace(".tt", "");
                Type templateType = Type.GetType($"CodeGen.{templateTypeFormat}, CodeGen");
                var template = Activator.CreateInstance(templateType, _codeGenConfig, model) as dynamic;
                string fileText = template.TransformText();

                _fileService.Create(filePath, fileText);
            }

            return Task.CompletedTask;
        }
    }
}
