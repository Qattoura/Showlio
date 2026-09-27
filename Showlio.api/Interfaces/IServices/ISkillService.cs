using Showlio.api.Dtos;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Interfaces.IServices
{
    public interface ISkillService 
    {
        Task<IEnumerable<Skill?>> GetAllSkillsForPortfolioAsync(int portfolioId);
        public Task<SkillServiceResult<Skill>> GetSkillAsync(int skillId, int portfolioId);
        Task<SkillServiceResult<Skill>> CreateAsync(CreateSkillDto createDto, int portfolioId);
        Task<SkillServiceResult<Skill>> UpdateAsync(UpdateSkillDto updateDto, int skillId,int portfolioId);
        Task<SkillServiceResult<bool>> DeleteAsync(int SkillId,int portfolioId);
    }
}
