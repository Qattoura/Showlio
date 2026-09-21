namespace Showlio.api.Models
{
    public class Education
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Institution { get; set; } = string.Empty;

        public string Degree { get; set; } = string.Empty;

        public string Field { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int DisplayOrder { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;

    }
}
