using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenOutput.API.DAL.Configurations
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100)
            ;

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100)
            ;

            builder.Property(x => x.Telephone)
                .HasMaxLength(100)
            ;

            builder.Property(x => x.Email)
                .HasMaxLength(100)
            ;
        }
    }
}
