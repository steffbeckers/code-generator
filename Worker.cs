using CodeGen.Generators;
using CodeGen.Models;
using CodeGen.Services;
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
        private readonly IConfigService _configService;
        private readonly IProjectsGenerator _projectsGenerator;

        public Worker(
            ILogger<Worker> logger,
            IConfigService configService,
            IProjectsGenerator projectsGenerator
        )
        {
            _logger = logger;
            _configService = configService;
            _projectsGenerator = projectsGenerator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // while (!stoppingToken.IsCancellationRequested)
            // {
            try
            {
                await _configService.Load();
                await _projectsGenerator.Generate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
            //     _logger.LogInformation("Waiting 10 seconds before regenerating");
            //     await Task.Delay(10000);
            // }
        }
    }
}
