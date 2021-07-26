using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace CodeGenOutput.EntityFrameworkCore
{
    [DependsOn(
        typeof(CodeGenOutputEntityFrameworkCoreModule)
        )]
    public class CodeGenOutputEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CodeGenOutputMigrationsDbContext>();
        }
    }
}
