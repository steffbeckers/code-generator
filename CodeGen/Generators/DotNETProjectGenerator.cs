using CodeGen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace CodeGen.Generators
{
    public class DotNETProjectGenerator : ProjectGenerator, IProjectGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DotNETProjectGenerator> _logger;
        private readonly IConfigService _configService;
        private readonly IFileService _fileService;

        public DotNETProjectGenerator(
            IConfiguration configuration,
            ILogger<DotNETProjectGenerator> logger,
            IConfigService configService,
            IFileService fileService
        ) : base(logger, configService, fileService)
        {
            _configuration = configuration;
            _logger = logger;
            _configService = configService;
            _fileService = fileService;
        }

        public override async Task Generate()
        {
            await base.Generate();

            // Output startup project path
            string startupProjectPath = Path.Combine(
                _outputProjectPath,
                _projectTemplateSettings.DotNET.StartupProjectPath
            );
            startupProjectPath = startupProjectPath.Replace('\\', '/');

            _logger.LogInformation("Buiding project: " + startupProjectPath);

            ProcessStartInfo dotnetBuild = new ProcessStartInfo("dotnet");
            dotnetBuild.Arguments = @"build";
            dotnetBuild.WorkingDirectory = startupProjectPath;
            Process dotnetBuildProcess = Process.Start(dotnetBuild);
            await dotnetBuildProcess.WaitForExitAsync();
            if (dotnetBuildProcess.ExitCode == 1)
            {
                throw new Exception("Project build failed. Stopping next after generate steps");
            }

            _logger.LogInformation("Recreating database/migrations: " + startupProjectPath);

            // Drop the existing database
            ProcessStartInfo dotnetDropDatabase = new ProcessStartInfo("dotnet");
            dotnetDropDatabase.Arguments = @"ef database drop --force --no-build";
            dotnetDropDatabase.WorkingDirectory = startupProjectPath;
            await Process.Start(dotnetDropDatabase).WaitForExitAsync();

            // Generate new Initial migration
            ProcessStartInfo dotnetAddInitialMigration = new ProcessStartInfo("dotnet");
            dotnetAddInitialMigration.Arguments = @"ef migrations add Initial --output-dir " + _projectTemplateSettings.DotNET.MigrationsFolderPath + " --no-build";
            dotnetAddInitialMigration.WorkingDirectory = startupProjectPath;
            await Process.Start(dotnetAddInitialMigration).WaitForExitAsync();

            if (_configuration.GetValue<bool>("Standalone"))
            {
                _logger.LogInformation("Executing standalone features");

                // Install project template with dotnet new after generate
                _logger.LogInformation("Installing project template: " + _outputProjectPath);

                ProcessStartInfo dotnetInstallProject = new ProcessStartInfo("dotnet");
                dotnetInstallProject.Arguments = @"new -i ./";
                dotnetInstallProject.WorkingDirectory = _outputProjectPath;
                await Process.Start(dotnetInstallProject).WaitForExitAsync();
            }
        }
    }
}
