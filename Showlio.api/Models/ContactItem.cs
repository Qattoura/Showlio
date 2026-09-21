namespace Showlio.api.Models
{
    public class ContactItem
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Label { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }

        // Navigation property
        public Portfolio Portfolio { get; set; } = null!;
    }
}
