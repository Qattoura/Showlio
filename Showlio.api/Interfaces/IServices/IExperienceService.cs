using Showlio.api.Dtos;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Interfaces.IServices
{
    public interface IExperienceService
    {
        Task<ExperienceServiceResult<IEnumerable<Experience>>> GetAllExperiencesForPortfolioAsync(int portfolioId);

        Task<ExperienceServiceResult<Experience>> GetExperienceAsync(int experienceId,int portfolioId);

        Task<ExperienceServiceResult<Experience>> CreateAsync(CreateExperienceDto createDto, int portfolioId);

        Task<ExperienceServiceResult<Experience>> UpdateAsync(UpdateExperienceDto updateDto, int experienceId,int portfolioId);

        Task<ExperienceServiceResult<bool>> DeleteAsync(int experienceId,int portfolioId);
    }
}
