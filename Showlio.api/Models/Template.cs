namespace Showlio.api.Models
{
    public class Template
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Genre { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string ColorTheme { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }
}
