using CodeGenOutput.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CodeGenOutput
{
    [DependsOn(
        typeof(CodeGenOutputEntityFrameworkCoreTestModule)
        )]
    public class CodeGenOutputDomainTestModule : AbpModule
    {

    }
}