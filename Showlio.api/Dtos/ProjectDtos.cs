namespace Showlio.api.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? ProjectLink { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class CreateProjectDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? ProjectLink { get; set; }
        public int DisplayOrder { get; set; }
    }

    public class UpdateProjectDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public string? ProjectLink { get; set; }
        public int DisplayOrder { get; set; }
    }
}
