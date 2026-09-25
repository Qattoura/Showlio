using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface IPortfolioRepository : IRepository<Portfolio>
    {
        Task<Portfolio?> GetByUserIdAsync(Guid userId);
        Task<bool> ExistsByUserIdAsync(Guid userId);
    }
}
