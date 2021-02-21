using CodeGen.Generators;
using CodeGen.Models;
using CodeGen.Services;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGen
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly IConfigService _configService;
        private readonly IProjectsGenerator _projectsGenerator;

        private HubConnection _realtimeConnection;

        public Worker(
            IConfiguration configuration,
            ILogger<Worker> logger,
            IConfigService configService,
            IProjectsGenerator projectsGenerator
        )
        {
            _configuration = configuration;
            _logger = logger;
            _configService = configService;
            _projectsGenerator = projectsGenerator;

            _logger.LogInformation($"API: {_configuration.GetValue<string>("API")}");
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _configService.LoadFromConfigFile();
                await _projectsGenerator.Generate();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw;
            }

            while (!cancellationToken.IsCancellationRequested && (this._realtimeConnection == null || this._realtimeConnection.State == HubConnectionState.Disconnected))
            {
                // Setup a connection to the realtime hub
                this._realtimeConnection = new HubConnectionBuilder()
                    .WithUrl(_configuration.GetValue<string>("API") + "/realtime-hub?isCodeGenerator=true")
                    .WithAutomaticReconnect()
                    .Build();

                // When the connection is closed
                this._realtimeConnection.Closed += async (ex) =>
                {
                    _logger.LogError("Lost realtime connection");
                    if (ex != null)
                        _logger.LogError(ex.ToString());

                    await Task.CompletedTask;
                };

                this._realtimeConnection.On("Generate", async (CodeGenConfig config) =>
                {
                    await _configService.LoadFromRequest(config);
                    await _projectsGenerator.Generate();
                });

                // Start initial realtime connection
                await Connect();

                // Keep client alive
                await Task.Delay(10000, cancellationToken);
            }
        }

        private async Task Connect()
        {
            try
            {
                // Don't connect again if already connected
                if (this._realtimeConnection.State == HubConnectionState.Disconnected)
                {
                    _logger.LogInformation("Connecting to realtime hub...");
                    await this._realtimeConnection.StartAsync();
                }

                if (this._realtimeConnection.State == HubConnectionState.Connected)
                    _logger.LogInformation("Connected.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Not able to connect. Is the API up and running? " + _configuration.GetValue<string>("API"));
                _logger.LogError(ex.ToString());
            }
        }
    }
}
