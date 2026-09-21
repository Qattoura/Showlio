using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
    {
        public void Configure(EntityTypeBuilder<Experience> builder)
        {
            builder
                .HasOne(e => e.Portfolio)
                .WithMany(p => p.Experiences)
                .HasForeignKey(e => e.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(e => e.Company)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(e => e.Position)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);
        }
    }
}