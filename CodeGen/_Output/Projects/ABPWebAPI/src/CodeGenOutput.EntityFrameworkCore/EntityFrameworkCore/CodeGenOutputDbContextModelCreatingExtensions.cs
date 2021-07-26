using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace CodeGenOutput.EntityFrameworkCore
{
    public static class CodeGenOutputDbContextModelCreatingExtensions
    {
        public static void ConfigureCodeGenOutput(this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(CodeGenOutputConsts.DbTablePrefix + "YourEntities", CodeGenOutputConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});
        }
    }
}