using System;
using CodeGen.Generators;
using CodeGen.Models;
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
                .ConfigureAppConfiguration((hostContext, config) => {
                    IHostEnvironment env = hostContext.HostingEnvironment;

                    config.SetBasePath(env.ContentRootPath);
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile("code-gen-config.json", optional: false, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IAppSettingsService, AppSettingsService>();
                    services.AddSingleton<IFileService, FileService>();
                    services.AddSingleton<IConfigBasedGenerator, ConfigBasedGenerator>();
                    services.AddSingleton<IModelsBasedGenerator, ModelsBasedGenerator>();
                    services.AddSingleton<IProjectsGenerator, ProjectsGenerator>();
                    services.AddHostedService<Worker>();
                });
    }
}
