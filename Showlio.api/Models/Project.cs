namespace Showlio.api.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? ProjectLink { get; set; }
        public int DisplayOrder { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;

    }
}
