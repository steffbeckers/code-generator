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
        Task Generate();
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

        public Task Generate()
        {
            foreach (var model in _codeGenConfig.Models)
            {
                string filePath = Path.Combine(_codeGenConfig.Paths.Output, "Projects", "WebAPI", "Models", $"{model.Name}.cs");
                string fileText = new Templates.Projects.WebAPI.Models.Model(model).TransformText();

                _fileService.Create(filePath, fileText);
            }

            return Task.CompletedTask;
        }
    }
}
