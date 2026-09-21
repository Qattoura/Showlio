using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class PortfolioConfiguration : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder
                .HasOne(p => p.Template)
                .WithMany(t => t.Portfolios)
                .HasForeignKey(p => p.TemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(p => p.PortfolioName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.JobTitle)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(p => p.ProfileImageReference)
                .HasMaxLength(500);
        }
    }
}