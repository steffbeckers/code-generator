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

        public Worker(
            ILogger<Worker> logger,
            IOptions<CodeGenConfig> codeGenConfigOptions
        )
        {
            _logger = logger;
            _codeGenConfig = codeGenConfigOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Name: {_codeGenConfig.Name}");
            _logger.LogInformation($"Description: {_codeGenConfig.Description}");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
