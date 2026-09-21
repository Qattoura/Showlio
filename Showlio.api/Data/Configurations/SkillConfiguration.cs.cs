using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class SkillConfiguration : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            builder
                .HasOne(s => s.Portfolio)
                .WithMany(p => p.Skills)
                .HasForeignKey(s => s.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}