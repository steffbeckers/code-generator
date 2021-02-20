using CodeGenOutput.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGenOutput.API.DAL.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
            ;

            builder.Property(x => x.Description)
                .HasMaxLength(100)
            ;

            builder.Property(x => x.TemplateName)
                .IsRequired()
                .HasMaxLength(100)
            ;
        }
    }
}
