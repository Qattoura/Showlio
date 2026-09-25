using System.ComponentModel.DataAnnotations;

namespace Showlio.api.Dtos
{
    public class PortfolioDtos
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        public string PortfolioName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ProfileImageReference { get; set; }

        public int TemplateId { get; set; }

        public bool IsPublished { get; set; }

        public bool IsSkillsEnabled { get; set; }
        public bool IsProjectsEnabled { get; set; }
        public bool IsExperienceEnabled { get; set; }
        public bool IsEducationEnabled { get; set; }
        public bool IsCertificatesEnabled { get; set; }
        public bool IsServicesEnabled { get; set; }
    }


    public class CreatePortfolioDto
    {
        public string PortfolioName { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public int TemplateId { get; set; }
    }

    public class UpdatePortfolioDto
    {
        public string PortfolioName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? ProfileImageReference { get; set; }
        public int TemplateId { get; set; }
        public bool IsPublished { get; set; }
        public bool IsSkillsEnabled { get; set; }
        public bool IsProjectsEnabled { get; set; }
        public bool IsExperienceEnabled { get; set; }
        public bool IsEducationEnabled { get; set; }
        public bool IsCertificatesEnabled { get; set; }
        public bool IsServicesEnabled { get; set; }
    }
}
