using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class ExperienceMappers
    {
        public static ExperienceDto ToDto(this Experience experience)
        {
            return new ExperienceDto
            {
                Id = experience.Id,
                PortfolioId = experience.PortfolioId,
                Company = experience.Company,
                Position = experience.Position,
                Description = experience.Description,
                StartDate = experience.StartDate,
                EndDate = experience.EndDate,
                IsCurrent = experience.IsCurrent,
                DisplayOrder = experience.DisplayOrder
            };
        }

        public static Experience ToEntity(
            this CreateExperienceDto dto,
            int portfolioId)
        {
            return new Experience
            {
                PortfolioId = portfolioId,
                Company = dto.Company,
                Position = dto.Position,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                IsCurrent = dto.IsCurrent,
                DisplayOrder = dto.DisplayOrder
            };
        }

        public static void ApplyUpdate(this Experience experience, UpdateExperienceDto dto)
        {
            experience.Company = dto.Company;
            experience.Position = dto.Position;
            experience.Description = dto.Description;
            experience.StartDate = dto.StartDate;
            experience.EndDate = dto.EndDate;
            experience.IsCurrent = dto.IsCurrent;
            experience.DisplayOrder = dto.DisplayOrder;
        }
    }
}