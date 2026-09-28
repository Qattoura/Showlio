using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface IEducationRepository : IRepository<Education>
    {
        Task<IEnumerable<Education>> GetAllForPortfolioAsync(
            int portfolioId);

        Task<Education?> GetByIdForPortfolioAsync(
            int educationId,
            int portfolioId);

        Task<int> CountEducationsForPortfolioAsync(
            int portfolioId);
    }
}
