using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Showlio.api.Repositories
{
    public class SkillRepository : Repository<Skill>, ISkillRepository
    {
        public SkillRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Skill>> GetAllForPortfolioAsync(int portfolioId)
        {
            return await _context.Skills.Where(s => s.PortfolioId == portfolioId).ToListAsync();
        }

        public async Task<Skill?> GetByIdForPortfolioAsync(int skillId, int portfolioId)
        {
            
            return await _context.Skills.FirstOrDefaultAsync(s => s.Id == skillId && s.PortfolioId == portfolioId);
        }

        public async Task<int> CountSkillsForPortfolioAsync(int portfolioId)
        {
            return await _context.Skills
                .CountAsync(s => s.PortfolioId == portfolioId);
        }
    }
}
