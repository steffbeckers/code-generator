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
    public interface IProjectGenerator
    {
        Task Generate();
    }

    public class ProjectGenerator : IProjectGenerator
    {
        private readonly ILogger<ProjectGenerator> _logger;
        private readonly CodeGenConfig _codeGenConfig;
        private readonly IFileService _fileService;
        private readonly IModelsGenerator _modelsGenerator;

        public ProjectGenerator(
            ILogger<ProjectGenerator> logger,
            IOptions<CodeGenConfig> codeGenConfigOptions,
            IFileService fileService,
            IModelsGenerator modelsGenerator
        )
        {
            _logger = logger;
            _codeGenConfig = codeGenConfigOptions.Value;
            _fileService = fileService;
            _modelsGenerator = modelsGenerator;
        }

        public Task Generate()
        {
            CleanupOutputDirectory();

            _logger.LogInformation("Generating projects");

            _modelsGenerator.Generate();

            return Task.CompletedTask;
        }

        private Task CleanupOutputDirectory()
        {
            _logger.LogInformation("Cleanup output directory");
            _fileService.DeleteDirectory(_codeGenConfig.Paths.Output);
            return Task.CompletedTask;
        }
    }
}
