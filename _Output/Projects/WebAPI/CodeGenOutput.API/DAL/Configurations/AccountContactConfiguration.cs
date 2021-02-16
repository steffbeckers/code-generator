using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenOutput.API.DAL.Configurations
{
    public class AccountContactConfiguration : IEntityTypeConfiguration<AccountContact>
    {
        public void Configure(EntityTypeBuilder<AccountContact> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.IsPrimary)
            ;

            builder.Property(x => x.SortOrder)
            ;
        }
    }
}
