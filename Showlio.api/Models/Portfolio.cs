using Showlio.api.Identity;

namespace Showlio.api.Models
{
    public class Portfolio
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

        // Sections enable
        public bool IsSkillsEnabled { get; set; }
        public bool IsProjectsEnabled { get; set; }
        public bool IsExperienceEnabled { get; set; }
        public bool IsEducationEnabled { get; set; }
        public bool IsCertificatesEnabled { get; set; }
        public bool IsServicesEnabled { get; set; }

        // Navigation property

        public Template Template { get; set; } = null!;

        public AppUser User { get; set; } = null!;

        public ICollection<Skill> Skills { get; set; } = new List<Skill>();
        public ICollection<Project> Projects { get; set; } = new List<Project>();
        public ICollection<Experience> Experiences { get; set; } = new List<Experience>();
        public ICollection<Education> Educations { get; set; } = new List<Education>();
        public ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();
        public ICollection<Service> Services { get; set; } = new List<Service>();
        public ICollection<ContactItem> ContactItems { get; set; } = new List<ContactItem>();

    }
}
