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

            // builder.Property(t => t.Title)
            //     .HasMaxLength(200)
            //     .IsRequired();
        }
    }
}
