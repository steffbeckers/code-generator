using System.Threading.Tasks;

namespace CodeGenOutput.Data
{
    public interface ICodeGenOutputDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
