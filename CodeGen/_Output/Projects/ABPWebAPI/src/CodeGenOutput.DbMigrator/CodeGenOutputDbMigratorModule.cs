using CodeGenOutput.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace CodeGenOutput.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(CodeGenOutputEntityFrameworkCoreDbMigrationsModule),
        typeof(CodeGenOutputApplicationContractsModule)
        )]
    public class CodeGenOutputDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
