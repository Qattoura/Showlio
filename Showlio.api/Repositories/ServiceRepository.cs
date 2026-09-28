using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Showlio.api.Repositories
{
    public class ServiceRepository
        : Repository<Service>, IServiceRepository
    {
        public ServiceRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Service>>
            GetAllForPortfolioAsync(int portfolioId)
        {
            return await _context.Services
                .Where(x => x.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<Service?>
            GetByIdForPortfolioAsync(
                int serviceId,
                int portfolioId)
        {
            return await _context.Services
                .FirstOrDefaultAsync(x =>
                    x.Id == serviceId &&
                    x.PortfolioId == portfolioId);
        }

        public async Task<int>
            CountServicesForPortfolioAsync(
                int portfolioId)
        {
            return await _context.Services
                .CountAsync(x =>
                    x.PortfolioId == portfolioId);
        }
    }
}
