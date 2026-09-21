namespace Showlio.api.Models
{
    public class Certificate
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? Image { get; set; }

        public string? CertificateLink { get; set; }

        public int DisplayOrder { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;

    }
}
