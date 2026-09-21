using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder
                .HasOne(e => e.Portfolio)
                .WithMany(p => p.Educations)
                .HasForeignKey(e => e.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(e => e.Institution)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(e => e.Degree)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(e => e.Field)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}