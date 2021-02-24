using CodeGen.Generators;
using CodeGen.Runners;
using CodeGen.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CodeGen
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    IHostEnvironment env = hostContext.HostingEnvironment;

                    config.SetBasePath(env.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile("code-gen-config.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IConfigService, ConfigService>();
                    services.AddSingleton<IFileService, FileService>();

                    switch (hostContext.Configuration.GetValue<string>("Template:Type"))
                    {
                        case "DotNET":
                            services.AddSingleton<IProjectGenerator, DotNETProjectGenerator>();
                            services.AddSingleton<IProjectRunner, DotNETProjectRunner>();
                            break;
                        default:
                            services.AddSingleton<IProjectGenerator, ProjectGenerator>();
                            services.AddSingleton<IProjectRunner, ProjectRunner>();
                            break;
                    }

                    services.AddHostedService<Worker>();
                });
    }
}
