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
        private readonly IAppSettingsService _appSettingsService;
        private readonly IProjectGenerator _projectGenerator;

        public Worker(
            ILogger<Worker> logger,
            IAppSettingsService appSettingsService,
            IProjectGenerator projectGenerator
        )
        {
            _logger = logger;
            _appSettingsService = appSettingsService;
            _projectGenerator = projectGenerator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await _appSettingsService.Load();
                    await _projectGenerator.Generate();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message, ex);
                    //throw;
                }

                _logger.LogInformation("Waiting 10 seconds before regenerating");
                await Task.Delay(10000);
            }
        }
    }
}
