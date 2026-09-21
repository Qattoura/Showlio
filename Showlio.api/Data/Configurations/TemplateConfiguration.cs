using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class TemplateConfiguration : IEntityTypeConfiguration<Template>
    {
        public void Configure(EntityTypeBuilder<Template> builder)
        {
            builder
                .HasIndex(t => t.Title)
                .IsUnique();

            builder
                .Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(t => t.Genre)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(t => t.ColorTheme)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}