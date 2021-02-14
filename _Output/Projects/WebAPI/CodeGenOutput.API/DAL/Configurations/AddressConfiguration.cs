using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenOutput.API.DAL.Configurations
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(100)
            ;

            builder.Property(x => x.HouseNumber)
                .IsRequired()
                .HasMaxLength(10)
            ;

            builder.Property(x => x.BoxNumber)
                .HasMaxLength(10)
            ;

            builder.Property(x => x.PostalCode)
                .IsRequired()
                .HasMaxLength(10)
            ;

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100)
            ;

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(100)
            ;
        }
    }
}
