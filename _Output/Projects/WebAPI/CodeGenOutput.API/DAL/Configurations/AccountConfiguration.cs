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

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
            ;

            builder.Property(x => x.Description)
                .HasMaxLength(4000)
            ;

            builder.Property(x => x.Telephone)
                .HasMaxLength(100)
            ;

            builder.Property(x => x.Email)
                .HasMaxLength(100)
            ;

            builder.Property(x => x.Website)
                .HasMaxLength(100)
            ;

            builder.Property(x => x.VAT)
                .HasMaxLength(100)
            ;
        }
    }
}
