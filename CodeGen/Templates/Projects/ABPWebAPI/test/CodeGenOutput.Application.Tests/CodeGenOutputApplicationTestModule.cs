using Volo.Abp.Modularity;

namespace CodeGenOutput
{
    [DependsOn(
        typeof(CodeGenOutputApplicationModule),
        typeof(CodeGenOutputDomainTestModule)
        )]
    public class CodeGenOutputApplicationTestModule : AbpModule
    {

    }
}