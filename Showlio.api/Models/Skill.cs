namespace Showlio.api.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;
    }
}
