namespace Showlio.api.Dtos
{
    public class CertificateDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? CertificateLink { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class CreateCertificateDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? Image { get; set; }

        public string? CertificateLink { get; set; }

        public int DisplayOrder { get; set; }
    }

    public class UpdateCertificateDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string? Image { get; set; }

        public string? CertificateLink { get; set; }

        public int DisplayOrder { get; set; }
    }
}