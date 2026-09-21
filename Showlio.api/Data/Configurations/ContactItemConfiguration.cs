using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Showlio.api.Models;

namespace Showlio.api.Data.Configurations
{
    public class ContactItemConfiguration : IEntityTypeConfiguration<ContactItem>
    {
        public void Configure(EntityTypeBuilder<ContactItem> builder)
        {
            builder
                .HasOne(c => c.Portfolio)
                .WithMany(p => p.ContactItems)
                .HasForeignKey(c => c.PortfolioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .Property(c => c.Label)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(c => c.Value)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}