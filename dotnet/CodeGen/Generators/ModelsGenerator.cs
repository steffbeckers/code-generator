using CodeGen.Models;
using CodeGen.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGen.Generators
{
    public interface IModelsGenerator
    {
        Task Generate(string projectTemplateFile, GenerateForEachModelData data);
    }

    public class ModelsGenerator : IModelsGenerator
    {
        private readonly ILogger<ModelsGenerator> _logger;
        private readonly CodeGenConfig _codeGenConfig;
        private readonly IFileService _fileService;

        public ModelsGenerator(
            ILogger<ModelsGenerator> logger,
            IOptions<CodeGenConfig> codeGenConfigOptions,
            IFileService fileService
        )
        {
            _logger = logger;
            _codeGenConfig = codeGenConfigOptions.Value;
            _fileService = fileService;
        }

        public Task Generate(string projectTemplateFile, GenerateForEachModelData data)
        {
            foreach (var model in _codeGenConfig.Models)
            {
                string filePath = Path.Combine(
                    _codeGenConfig.Paths.Output,
                    Path.GetDirectoryName(projectTemplateFile),
                    string.Format(data.Output, model.Name)
                );
                filePath = filePath.Replace("Templates\\", "");

                string templateTypeFormat = projectTemplateFile.Replace("\\", ".").Replace(".tt", "");
                Type templateType = Type.GetType($"CodeGen.{templateTypeFormat}, CodeGen");
                var template = Activator.CreateInstance(templateType, model) as dynamic;

                string fileText = template.TransformText();

                _fileService.Create(filePath, fileText);

                _logger.LogInformation(filePath);
            }

            return Task.CompletedTask;
        }
    }
}
