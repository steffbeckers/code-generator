using CodeGen.Generators;
using CodeGen.Models;
using CodeGen.Services;
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
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<CodeGenConfig>(hostContext.Configuration.GetSection("CodeGenConfig"));
                    services.AddSingleton<IFileService, FileService>();
                    services.AddSingleton<IConfigBasedGenerator, ConfigBasedGenerator>();
                    services.AddSingleton<IModelsBasedGenerator, ModelsBasedGenerator>();
                    services.AddSingleton<IProjectGenerator, ProjectGenerator>();
                    services.AddHostedService<Worker>();
                });
    }
}
