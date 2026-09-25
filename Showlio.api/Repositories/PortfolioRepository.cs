using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Showlio.api.Repositories
{
    public class PortfolioRepository
        : Repository<Portfolio>, IPortfolioRepository
    {
        public PortfolioRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ExistsByUserIdAsync(Guid userId)
        {

            return await _entity.AnyAsync(p => p.UserId == userId);
        }

        public async Task<Portfolio?> GetByUserIdAsync(Guid userId)
        {
            return await _entity
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }
    }
}
