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
        private readonly ILogger<Worker> _logger;
        private readonly IConfigService _configService;
        private readonly IProjectGenerator _projectGenerator;
        private readonly IProjectRunner _projectRunner;

        private HubConnection _realtimeConnection;

        public Worker(
            ILogger<Worker> logger,
            IConfigService configService,
            IProjectGenerator projectGenerator,
            IProjectRunner projectRunner
        )
        {
            _logger = logger;
            _configService = configService;
            _projectGenerator = projectGenerator;
            _projectRunner = projectRunner;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            bool standalone = _configService.AppSettings.GetValue<bool>("Standalone");
            if (standalone)
            {
                // Standalone
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
                // API
                while (!cancellationToken.IsCancellationRequested && (this._realtimeConnection == null || this._realtimeConnection.State == HubConnectionState.Disconnected))
                {
                    // Setup a connection to the realtime hub
                    _realtimeConnection = new HubConnectionBuilder()
                        .WithUrl($"{_configService.AppSettings.GetValue<string>("API:URL")}/realtime-hub?isCodeGenerator=true&Template={_configService.AppSettings.GetValue<string>("Template:Name")}")
                        .WithAutomaticReconnect()
                        .Build();

                    // When the connection is closed
                    _realtimeConnection.Closed += async (ex) =>
                    {
                        _logger.LogError("Lost realtime connection");
                        if (ex != null)
                            _logger.LogError(ex.ToString());

                        await Task.CompletedTask;
                    };

                    _realtimeConnection.On("Generate", async (CodeGenConfig config) =>
                    {
                        await _configService.UpdateConfig(config);
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
                if (_realtimeConnection.State == HubConnectionState.Disconnected)
                {
                    _logger.LogInformation("Connecting to realtime hub...");
                    await _realtimeConnection.StartAsync();
                }

                if (_realtimeConnection.State == HubConnectionState.Connected)
                    _logger.LogInformation("Connected.");
            }
            catch (Exception ex)
            {
                _logger.LogError("Not able to connect. Is the API up and running? " + _configService.AppSettings.GetValue<string>("API:URL"));
                _logger.LogError(ex.ToString());
            }
        }
    }
}
