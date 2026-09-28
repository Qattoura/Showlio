namespace Showlio.api.Dtos
{
    public class ServiceDto
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? Link { get; set; }

        public int DisplayOrder { get; set; }
    }

    public class CreateServiceDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? Link { get; set; }

        public int DisplayOrder { get; set; }
    }

    public class UpdateServiceDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? Link { get; set; }

        public int DisplayOrder { get; set; }
    }
}
