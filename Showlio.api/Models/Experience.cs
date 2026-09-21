namespace Showlio.api.Models
{
    public class Experience
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Company { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public int DisplayOrder { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;
    }
}
