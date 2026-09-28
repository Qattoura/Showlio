namespace Showlio.api.Dtos
{
    public class ContactItemDto
    {
        public int Id { get; set; }

        public int PortfolioId { get; set; }

        public string Label { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }

    public class CreateContactItemDto
    {
        public string Label { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }

    public class UpdateContactItemDto
    {
        public string Label { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }



}
