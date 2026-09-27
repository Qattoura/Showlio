using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<IEnumerable<Project>> GetAllForPortfolioAsync(int portfolioId);
        Task<Project?> GetByIdForPortfolioAsync(int ProjectId, int portfolioId);
        Task<int> CountProjectsForPortfolioAsync(int portfolioId);
    }
}
