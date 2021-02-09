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
            
            // builder.Property(t => t.Title)
            //     .HasMaxLength(200)
            //     .IsRequired();
        }
    }
}
