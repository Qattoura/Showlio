using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class CertificateConfiguration : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> builder)
        {
            builder
                .HasOne(c => c.Portfolio)
                .WithMany(p => p.Certificates)
                .HasForeignKey(c => c.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(c => c.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(c => c.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(c => c.Image)
                .HasMaxLength(500);

            builder
                .Property(c => c.CertificateLink)
                .HasMaxLength(500);
        }
    }
}