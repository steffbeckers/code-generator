using CodeGen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CodeGen.Runners
{
    public class AngularProjectRunner : ProjectRunner, IProjectRunner
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AngularProjectRunner> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;

        public AngularProjectRunner(
            IConfiguration configuration,
            ILogger<AngularProjectRunner> logger,
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

            // Start project
            ProcessStartInfo angularRun = new ProcessStartInfo(_configService.AppSettings.GetValue<string>("Paths:NPM"));
            angularRun.Arguments = @"start -- --host 0.0.0.0 --disable-host-check";
            angularRun.WorkingDirectory = _outputProjectPath;
            await Process.Start(angularRun).WaitForExitAsync();
        }
    }
}
