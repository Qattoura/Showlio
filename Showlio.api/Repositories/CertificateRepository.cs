using Microsoft.EntityFrameworkCore;
using Showlio.api.Data;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Models;

namespace Showlio.api.Repositories
{
    public class CertificateRepository
        : Repository<Certificate>, ICertificateRepository
    {
        public CertificateRepository(ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Certificate>>GetAllForPortfolioAsync(int portfolioId)
        {
            return await _context.Certificates
                .AsNoTracking()
                .Where(c => c.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<Certificate?> GetByIdForPortfolioAsync(
            int certificateId,
            int portfolioId)
        {
            return await _context.Certificates
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.Id == certificateId &&
                    c.PortfolioId == portfolioId);
        }

        public async Task<int> CountCertificatesForPortfolioAsync(
            int portfolioId)
        {
            return await _context.Certificates
                .CountAsync(c => c.PortfolioId == portfolioId);
        }
    }
}