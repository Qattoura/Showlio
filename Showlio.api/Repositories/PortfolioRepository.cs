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

        public async Task<bool> IsOwnedByUserAsync(int portfolioId, Guid userId)
        {
            return await _context.Portfolios
                .AnyAsync(p => p.Id == portfolioId && p.UserId == userId);
        }


        public async Task<Portfolio?> GetByUserIdAsync(Guid userId)
        {
            return await _entity
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task<Portfolio?> GetPublishedByUsernameAsync(string username)
        {
            return await _context.Portfolios
                .AsNoTracking()
                .AsSplitQuery()
                .Include(p => p.Skills)
                .Include(p => p.Projects)
                .Include(p => p.Experiences)
                .Include(p => p.Educations)
                .Include(p => p.Certificates)
                .Include(p => p.Services)
                .Include(p => p.ContactItems)
                .FirstOrDefaultAsync(p =>
                    p.User.UserName == username &&
                    p.IsPublished);
        }


    }
}
