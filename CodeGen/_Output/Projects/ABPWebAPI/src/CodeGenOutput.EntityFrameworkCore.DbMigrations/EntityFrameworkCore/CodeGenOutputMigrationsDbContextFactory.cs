using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CodeGenOutput.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class CodeGenOutputMigrationsDbContextFactory : IDesignTimeDbContextFactory<CodeGenOutputMigrationsDbContext>
    {
        public CodeGenOutputMigrationsDbContext CreateDbContext(string[] args)
        {
            CodeGenOutputEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<CodeGenOutputMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new CodeGenOutputMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CodeGenOutput.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
