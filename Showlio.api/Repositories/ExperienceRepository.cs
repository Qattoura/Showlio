using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Showlio.api.Repositories
{
    public class ExperienceRepository : Repository<Experience>, IExperienceRepository
    {
        public ExperienceRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Experience>> GetAllForPortfolioAsync(int portfolioId)
        {
            return await _context.Experiences.AsNoTracking()
                .Where(e => e.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<Experience?> GetByIdForPortfolioAsync(
            int experienceId,
            int portfolioId)
        {
            return await _context.Experiences.AsNoTracking()
                .FirstOrDefaultAsync(e =>
                    e.Id == experienceId &&
                    e.PortfolioId == portfolioId);
        }

        public async Task<int> CountExperiencesForPortfolioAsync(int portfolioId)
        {
            return await _context.Experiences
                .CountAsync(e => e.PortfolioId == portfolioId);
        }
    }
}