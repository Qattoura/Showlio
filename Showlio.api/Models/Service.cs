namespace Showlio.api.Models
{
    public class Service
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? Link { get; set; }

        public int DisplayOrder { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;
    }
}
