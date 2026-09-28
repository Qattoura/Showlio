using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface IContactItemRepository : IRepository<ContactItem>
    {
        Task<IEnumerable<ContactItem>> GetAllForPortfolioAsync(
            int portfolioId);

        Task<ContactItem?> GetByIdForPortfolioAsync(
            int contactItemId,
            int portfolioId);

        Task<int> CountContactItemsForPortfolioAsync(
            int portfolioId);
    }
}
