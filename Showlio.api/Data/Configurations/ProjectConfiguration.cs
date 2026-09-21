using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .HasOne(p => p.Portfolio)
                .WithMany(p => p.Projects)
                .HasForeignKey(p => p.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(p => p.Image)
                .HasMaxLength(500);

            builder
                .Property(p => p.ProjectLink)
                .HasMaxLength(500);
        }
    }
}