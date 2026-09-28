using Showlio.api.Dtos;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Interfaces.IServices
{
    public interface IEducationService
    {
        Task<EducationServiceResult<IEnumerable<Education>>>
            GetAllEducationsForPortfolioAsync(int portfolioId);

        Task<EducationServiceResult<Education>>
            GetEducationAsync(
                int educationId,
                int portfolioId);

        Task<EducationServiceResult<Education>>
            CreateAsync(
                CreateEducationDto createDto,
                int portfolioId);

        Task<EducationServiceResult<Education>>
            UpdateAsync(
                UpdateEducationDto updateDto,
                int educationId,
                int portfolioId);

        Task<EducationServiceResult<bool>>
            DeleteAsync(
                int educationId,
                int portfolioId);
    }
}
