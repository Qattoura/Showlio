using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class EducationMappers
    {
        public static EducationDto ToDto(this Education education)
        {
            return new EducationDto
            {
                Id = education.Id,
                PortfolioId = education.PortfolioId,
                Institution = education.Institution,
                Degree = education.Degree,
                Field = education.Field,
                StartDate = education.StartDate,
                EndDate = education.EndDate,
                DisplayOrder = education.DisplayOrder
            };
        }

        public static Education ToEntity(
            this CreateEducationDto dto,
            int portfolioId)
        {
            return new Education
            {
                PortfolioId = portfolioId,
                Institution = dto.Institution,
                Degree = dto.Degree,
                Field = dto.Field,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                DisplayOrder = dto.DisplayOrder
            };
        }

        public static void ApplyUpdate(
            this Education education,
            UpdateEducationDto dto)
        {
            education.Institution = dto.Institution;
            education.Degree = dto.Degree;
            education.Field = dto.Field;
            education.StartDate = dto.StartDate;
            education.EndDate = dto.EndDate;
            education.DisplayOrder = dto.DisplayOrder;
        }
    }
}