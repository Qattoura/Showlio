using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface IServiceRepository : IRepository<Service>
    {
        Task<IEnumerable<Service>> GetAllForPortfolioAsync(int portfolioId);

        Task<Service?> GetByIdForPortfolioAsync(int serviceId,int portfolioId);

        Task<int> CountServicesForPortfolioAsync(int portfolioId);
    }
}
