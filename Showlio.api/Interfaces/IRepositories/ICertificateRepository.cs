using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface ICertificateRepository : IRepository<Certificate>
    {
        Task<IEnumerable<Certificate>> GetAllForPortfolioAsync(int portfolioId);

        Task<Certificate?> GetByIdForPortfolioAsync(int certificateId, int portfolioId);

        Task<int> CountCertificatesForPortfolioAsync(int portfolioId);
    }
}