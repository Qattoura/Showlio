using Microsoft.EntityFrameworkCore;
using Showlio.api.Data;
using Showlio.api.Interfaces;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Models;

namespace Showlio.api.Repositories
{
    public class EducationRepository
        : Repository<Education>, IEducationRepository
    {
        public EducationRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Education>>
            GetAllForPortfolioAsync(int portfolioId)
        {
            return await _context.Educations.AsNoTracking()
                .Where(x => x.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<Education?>
            GetByIdForPortfolioAsync(
                int educationId,
                int portfolioId)
        {
            return await _context.Educations.AsNoTracking()
                .FirstOrDefaultAsync(x =>
                    x.Id == educationId &&
                    x.PortfolioId == portfolioId);
        }

        public async Task<int>
            CountEducationsForPortfolioAsync(
                int portfolioId)
        {
            return await _context.Educations
                .CountAsync(x =>
                    x.PortfolioId == portfolioId);
        }
    }
}