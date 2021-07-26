using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using CodeGenOutput.Data;
using Volo.Abp.DependencyInjection;

namespace CodeGenOutput.EntityFrameworkCore
{
    public class EntityFrameworkCoreCodeGenOutputDbSchemaMigrator
        : ICodeGenOutputDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreCodeGenOutputDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the CodeGenOutputMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<CodeGenOutputMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}