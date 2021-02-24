using CodeGen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CodeGen.Runners
{
    public class DotNETProjectRunner : ProjectRunner, IProjectRunner
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DotNETProjectRunner> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;

        public DotNETProjectRunner(
            IConfiguration configuration,
            ILogger<DotNETProjectRunner> logger,
            IConfigService configService,
            IFileService fileService
        ) : base(logger, configService, fileService)
        {
            _configuration = configuration;
            _logger = logger;
            _configService = configService;
            _fileService = fileService;
        }

        public new async Task Run()
        {
            await base.Run();

            // Output startup project path
            string startupProjectPath = Path.Combine(
                _outputProjectPath,
                _projectTemplateSettings.DotNET.StartupProjectPath
            );
            startupProjectPath = startupProjectPath.Replace('\\', '/');

            if (_configuration.GetValue<bool>("Standalone"))
            {
                _logger.LogInformation("Executing standalone features");

                // Open swagger
                _logger.LogInformation("Opening Swagger docs: " + _outputProjectPath);

                ProcessStartInfo openSwagger = new ProcessStartInfo("explorer");
                openSwagger.Arguments = _projectTemplateSettings.DotNET.StartupProjectURL;
                await Process.Start(openSwagger).WaitForExitAsync();
            }

            // Start project
            ProcessStartInfo dotnetRun = new ProcessStartInfo("dotnet");
            // TODO: Configurable --urls param
            //dotnetRun.Arguments = @"run";
            dotnetRun.Arguments = @"run --urls http://0.0.0.0:5001";
            dotnetRun.WorkingDirectory = startupProjectPath;
            await Process.Start(dotnetRun).WaitForExitAsync();
        }
    }
}
