using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace CodeGenOutput.Data
{
    /* This is used if database provider does't define
     * ICodeGenOutputDbSchemaMigrator implementation.
     */
    public class NullCodeGenOutputDbSchemaMigrator : ICodeGenOutputDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}