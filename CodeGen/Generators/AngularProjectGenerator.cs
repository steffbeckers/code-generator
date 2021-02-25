using CodeGen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CodeGen.Generators
{
    public class AngularProjectGenerator : ProjectGenerator, IProjectGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AngularProjectGenerator> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;

        public AngularProjectGenerator(
            IConfiguration configuration,
            ILogger<AngularProjectGenerator> logger,
            IConfigService configService,
            IFileService fileService
        ) : base(logger, configService, fileService)
        {
            _configuration = configuration;
            _logger = logger;
            _configService = configService;
            _fileService = fileService;
        }

        public new async Task Generate()
        {
            await base.Generate();

            _logger.LogInformation("Installing dependencies");

            ProcessStartInfo npmInstall = new ProcessStartInfo(_configService.AppSettings.GetValue<string>("Paths:NPM"));
            npmInstall.Arguments = @"install";
            npmInstall.WorkingDirectory = _outputProjectPath;
            await Process.Start(npmInstall).WaitForExitAsync();

            ProcessStartInfo npmBuild = new ProcessStartInfo(_configService.AppSettings.GetValue<string>("Paths:NPM"));
            npmBuild.Arguments = @"run build";
            npmBuild.WorkingDirectory = _outputProjectPath;
            await Process.Start(npmBuild).WaitForExitAsync();
        }
    }
}
