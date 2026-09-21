using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder
                .HasOne(s => s.Portfolio)
                .WithMany(p => p.Services)
                .HasForeignKey(s => s.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(s => s.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(s => s.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(s => s.Link)
                .HasMaxLength(500);
        }
    }
}