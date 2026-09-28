namespace Showlio.api.Dtos
{
    public class ExperienceDto
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
    }

    public class CreateExperienceDto
    {
        public string Company { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public int DisplayOrder { get; set; }
    }

    public class UpdateExperienceDto
    {
        public string Company { get; set; } = string.Empty;

        public string Position { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsCurrent { get; set; }

        public int DisplayOrder { get; set; }
    }
}