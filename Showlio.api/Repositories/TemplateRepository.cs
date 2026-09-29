using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Showlio.api.Repositories
{
    public class TemplateRepository : Repository<Template>, ITemplateRepository
    {
        public TemplateRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Template>> GetActiveTemplatesAsync()
        {
            return await _context.Templates.Where(t => t.IsActive).ToListAsync();
        }

        public async Task<Template?> GetActiveTemplateByIdAsync(int id)
        {
            return await _context.Templates
                .FirstOrDefaultAsync(t =>
                    t.Id == id &&
                    t.IsActive);
        }

        public async Task<bool> ExistsByTitleAsync(string title, int? excludeId = null)
        {
            return await _context.Templates
                .AnyAsync(t =>
                    t.Title == title &&
                    (!excludeId.HasValue || t.Id != excludeId.Value));
        }

        public async Task<bool> IsTemplateUsedAsync(int templateId)
        {
            return await _context.Portfolios
                .AnyAsync(p => p.TemplateId == templateId);
        }

    }
}
