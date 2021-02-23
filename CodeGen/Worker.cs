using CodeGen.Generators;
using CodeGen.Models;
using CodeGen.Runners;
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
        private readonly IProjectGenerator _projectGenerator;
        private readonly IProjectRunner _projectRunner;

        private HubConnection _realtimeConnection;

        public Worker(
            IConfiguration configuration,
            ILogger<Worker> logger,
            IConfigService configService,
            IProjectGenerator projectGenerator,
            IProjectRunner projectRunner
        )
        {
            _configuration = configuration;
            _logger = logger;
            _configService = configService;
            _projectGenerator = projectGenerator;
            _projectRunner = projectRunner;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            bool standalone = _configuration.GetValue<bool>("Standalone");
            if (standalone)
            {
                try
                {
                    await _configService.LoadFromConfigFile();
                    await _projectGenerator.Generate();
                    await _projectRunner.Run();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
            }
            else
            {
                _logger.LogInformation($"API: {_configuration.GetValue<string>("API")}");

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

                    this._realtimeConnection.On("Generate", async (Project project) =>
                    {
                        await _configService.LoadFromRequest(project.Config);
                    });

                    // Start initial realtime connection
                    await Connect();

                    try
                    {
                        await _configService.LoadFromConfigFile();
                        await _projectGenerator.Generate();
                        _projectRunner.Run();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.Message);
                        throw;
                    }
                }
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
