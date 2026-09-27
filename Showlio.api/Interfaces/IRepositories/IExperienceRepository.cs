using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface IExperienceRepository : IRepository<Experience>
    {
        Task<IEnumerable<Experience>> GetAllForPortfolioAsync(int portfolioId);
        Task<Experience?> GetByIdForPortfolioAsync(int experienceId, int portfolioId);
        Task<int> CountExperiencesForPortfolioAsync(int portfolioId);
    }
}
