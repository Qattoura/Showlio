using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Showlio.api.Repositories
{
    public class ContactItemRepository
        : Repository<ContactItem>, IContactItemRepository
    {
        public ContactItemRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<ContactItem>>
            GetAllForPortfolioAsync(int portfolioId)
        {
            return await _context.ContactItems
                .Where(x => x.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<ContactItem?>
            GetByIdForPortfolioAsync(
                int contactItemId,
                int portfolioId)
        {
            return await _context.ContactItems
                .FirstOrDefaultAsync(x =>
                    x.Id == contactItemId &&
                    x.PortfolioId == portfolioId);
        }

        public async Task<int>
            CountContactItemsForPortfolioAsync(
                int portfolioId)
        {
            return await _context.ContactItems
                .CountAsync(x =>
                    x.PortfolioId == portfolioId);
        }
    }
}
