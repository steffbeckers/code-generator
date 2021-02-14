using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenOutput.API.DAL.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasDefaultValue()
                .HasColumnType("nvarchar(100)")
            ;
        }
    }
}
