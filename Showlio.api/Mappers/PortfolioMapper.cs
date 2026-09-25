using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class PortfolioMapper
    {
        public static PortfolioDtos ToDto(this Portfolio portfolio)
        {
            return new PortfolioDtos
            {
                Id = portfolio.Id,
                UserId = portfolio.UserId,
                PortfolioName = portfolio.PortfolioName,
                Name = portfolio.Name,
                JobTitle = portfolio.JobTitle,
                Description = portfolio.Description,
                ProfileImageReference = portfolio.ProfileImageReference,
                TemplateId = portfolio.TemplateId,
                IsPublished = portfolio.IsPublished,
                IsSkillsEnabled = portfolio.IsSkillsEnabled,
                IsProjectsEnabled = portfolio.IsProjectsEnabled,
                IsExperienceEnabled = portfolio.IsExperienceEnabled,
                IsEducationEnabled = portfolio.IsEducationEnabled,
                IsCertificatesEnabled = portfolio.IsCertificatesEnabled,
                IsServicesEnabled = portfolio.IsServicesEnabled,
            };
        }


        public static void ApplyUpdate(this Portfolio portfolio, UpdatePortfolioDto dto)
        {
            portfolio.PortfolioName = dto.PortfolioName;
            portfolio.Name = dto.Name;
            portfolio.JobTitle = dto.JobTitle;
            portfolio.Description = dto.Description;
            portfolio.ProfileImageReference = dto.ProfileImageReference;
            portfolio.TemplateId = dto.TemplateId;
            portfolio.IsPublished = dto.IsPublished;
            portfolio.IsSkillsEnabled = dto.IsSkillsEnabled;
            portfolio.IsProjectsEnabled = dto.IsProjectsEnabled;
            portfolio.IsExperienceEnabled = dto.IsExperienceEnabled;
            portfolio.IsEducationEnabled = dto.IsEducationEnabled;
            portfolio.IsCertificatesEnabled = dto.IsCertificatesEnabled;
            portfolio.IsServicesEnabled = dto.IsServicesEnabled;
        }


    }
}
