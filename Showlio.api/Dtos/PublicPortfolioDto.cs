namespace Showlio.api.Dtos
{
    public class PublicPortfolioDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        public string PortfolioName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ProfileImageReference { get; set; }

        public int TemplateId { get; set; }

        public bool IsSkillsEnabled { get; set; }
        public bool IsProjectsEnabled { get; set; }
        public bool IsExperienceEnabled { get; set; }
        public bool IsEducationEnabled { get; set; }
        public bool IsCertificatesEnabled { get; set; }
        public bool IsServicesEnabled { get; set; }

        public IEnumerable<SkillDto> Skills { get; set; }
            = new List<SkillDto>();

        public IEnumerable<ProjectDto> Projects { get; set; }
            = new List<ProjectDto>();

        public IEnumerable<ExperienceDto> Experiences { get; set; }
            = new List<ExperienceDto>();

        public IEnumerable<EducationDto> Educations { get; set; }
            = new List<EducationDto>();

        public IEnumerable<CertificateDto> Certificates { get; set; }
            = new List<CertificateDto>();

        public IEnumerable<ServiceDto> Services { get; set; }
            = new List<ServiceDto>();

        public IEnumerable<ContactItemDto> ContactItems { get; set; }
            = new List<ContactItemDto>();
    }
}
