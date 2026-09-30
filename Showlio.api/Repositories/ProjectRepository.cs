using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Showlio.api.Repositories
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Project>> GetAllForPortfolioAsync(int portfolioId)
        {
            return await _context.Projects.AsNoTracking().Where(p => p.PortfolioId == portfolioId).ToListAsync();

        }

        public async Task<Project?> GetByIdForPortfolioAsync(int projectId, int portfolioId)
        {
            return await _context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == projectId && p.PortfolioId == portfolioId);
        }
        public async Task<int> CountProjectsForPortfolioAsync(int portfolioId)
        {
            return await _context.Projects.CountAsync(p => p.PortfolioId == portfolioId);
        }
    }
}
