using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<IEnumerable<Skill>> GetAllForPortfolioAsync(int portfolioId);
        Task<Skill?> GetByIdForPortfolioAsync(int skillId, int portfolioId);
        Task<int> CountSkillsForPortfolioAsync(int portfolioId);
    }
}
