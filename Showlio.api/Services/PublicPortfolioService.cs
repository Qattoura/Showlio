using Showlio.api.Dtos;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;

namespace Showlio.api.Services
{
    public class PublicPortfolioService : IPublicPortfolioService
    {

        private readonly IPortfolioRepository _portfolioRepo;

        public PublicPortfolioService(IPortfolioRepository portfolioRepo)
        {
            _portfolioRepo = portfolioRepo;
        }



        public async Task<PublicPortfolioDto?>GetPublishedPortfolioByUsernameAsync(string username)
        {
            

            var portfolio =
                await _portfolioRepo.GetPublishedByUsernameAsync(username);

            if (portfolio == null)
            {
                return null;
            }

            return new PublicPortfolioDto
            {
                Id = portfolio.Id,
                UserId = portfolio.UserId,

                PortfolioName = portfolio.PortfolioName,
                Name = portfolio.Name,
                JobTitle = portfolio.JobTitle,
                Description = portfolio.Description,
                ProfileImageReference = portfolio.ProfileImageReference,

                TemplateId = portfolio.TemplateId,

                IsSkillsEnabled = portfolio.IsSkillsEnabled,
                IsProjectsEnabled = portfolio.IsProjectsEnabled,
                IsExperienceEnabled = portfolio.IsExperienceEnabled,
                IsEducationEnabled = portfolio.IsEducationEnabled,
                IsCertificatesEnabled = portfolio.IsCertificatesEnabled,
                IsServicesEnabled = portfolio.IsServicesEnabled,

                Skills = portfolio.IsSkillsEnabled
                    ? portfolio.Skills.Select(s => s.ToDto())
                    : Enumerable.Empty<SkillDto>(),

                Projects = portfolio.IsProjectsEnabled
                    ? portfolio.Projects.Select(p => p.ToDto())
                    : Enumerable.Empty<ProjectDto>(),

                Experiences = portfolio.IsExperienceEnabled
                    ? portfolio.Experiences.Select(e => e.ToDto())
                    : Enumerable.Empty<ExperienceDto>(),

                Educations = portfolio.IsEducationEnabled
                    ? portfolio.Educations.Select(e => e.ToDto())
                    : Enumerable.Empty<EducationDto>(),

                Certificates = portfolio.IsCertificatesEnabled
                    ? portfolio.Certificates.Select(c => c.ToDto())
                    : Enumerable.Empty<CertificateDto>(),

                Services = portfolio.IsServicesEnabled
                    ? portfolio.Services.Select(s => s.ToDto())
                    : Enumerable.Empty<ServiceDto>(),

                ContactItems = portfolio.ContactItems.Select(c => c.ToDto())
            };
        }
    }
}
