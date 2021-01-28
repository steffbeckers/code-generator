using CodeGen.Generators;
using CodeGen.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGen
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly CodeGenConfig _codeGenConfig;
        private readonly IProjectGenerator _projectGenerator;

        public Worker(
            ILogger<Worker> logger,
            IOptions<CodeGenConfig> codeGenConfigOptions,
            IProjectGenerator projectGenerator
        )
        {
            _logger = logger;
            _projectGenerator = projectGenerator;
            _codeGenConfig = codeGenConfigOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Name: {_codeGenConfig.Name}");
            _logger.LogInformation($"Description: {_codeGenConfig.Description}");

            while (!stoppingToken.IsCancellationRequested)
            {
                await _projectGenerator.Generate();
                await Task.Delay(5000);
            }

            _logger.LogInformation("Done");
        }
    }
}
