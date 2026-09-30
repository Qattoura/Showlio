
using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Globals;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Models;
using Showlio.api.Results;



namespace Showlio.api.Services
{
    public class SkillService : ISkillService
    {

        private readonly ISkillRepository _skillRepo;
        private readonly IPortfolioAuthorizationService _portfolioAuthoService;
        public SkillService(ISkillRepository skillRepository,
            IPortfolioAuthorizationService portfolioAuthorizationService) 
        {
            _skillRepo = skillRepository;
            _portfolioAuthoService = portfolioAuthorizationService;

        }

        // return how many skills per portfolio
        private async Task<int> GetSkillCountAsync(int portfolioId)
        {
            return await _skillRepo.CountSkillsForPortfolioAsync(portfolioId);
        }

        public async Task<SkillServiceResult<IEnumerable<Skill>>> GetAllSkillsForPortfolioAsync(int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new SkillServiceResult<IEnumerable<Skill>>(
                    SkillServiceStatus.PortfolioNotOwned);
            }

            var skills = await _skillRepo.GetAllForPortfolioAsync(portfolioId);

            return new SkillServiceResult<IEnumerable<Skill>>(SkillServiceStatus.Success, skills);
        }

        public async Task<SkillServiceResult<Skill>> GetSkillAsync(int skillId, int portfolioId)
        {

            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new SkillServiceResult<Skill>(SkillServiceStatus.PortfolioNotOwned);
            }

            var skill = await _skillRepo.GetByIdForPortfolioAsync(skillId, portfolioId);

            if (skill == null)
            {
                return new SkillServiceResult<Skill>(SkillServiceStatus.SkillNotFound);
            }

            return new SkillServiceResult<Skill>(SkillServiceStatus.Success, skill);
        }



        public async Task<SkillServiceResult<Skill>> CreateAsync(CreateSkillDto createDto, int portfolioId)
        {
            // User owns the portfolio
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new SkillServiceResult<Skill>(
                    SkillServiceStatus.PortfolioNotOwned);
            }

            // Maximum 10 skills
            if (await GetSkillCountAsync(portfolioId) >= GlobalConstants.SkillMax)
            {
                return new SkillServiceResult<Skill>(
                    SkillServiceStatus.SkillLimitReached);
            }

            var skill = await _skillRepo.CreateAsync(createDto.ToEntity(portfolioId));

            await _skillRepo.SaveChangesAsync();

            return new SkillServiceResult<Skill>(SkillServiceStatus.Success, skill);
        }



        public async Task<SkillServiceResult<Skill>> UpdateAsync(UpdateSkillDto updateDto, int skillId, int portfolioId)
        {

            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new SkillServiceResult<Skill>(SkillServiceStatus.PortfolioNotOwned);
            }

            var skill = await _skillRepo.GetByIdForPortfolioAsync(skillId, portfolioId);

            if (skill == null)
            {
                return new SkillServiceResult<Skill>(SkillServiceStatus.SkillNotFound);
            }

            skill.ApplyUpdate(updateDto);

            _skillRepo.Update(skill);
            await _skillRepo.SaveChangesAsync();

            return new SkillServiceResult<Skill>(SkillServiceStatus.Success, skill);
        }

        public async Task<SkillServiceResult<bool>> DeleteAsync(int skillId, int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new SkillServiceResult<bool>(SkillServiceStatus.PortfolioNotOwned);
            }

            var skill = await _skillRepo.GetByIdForPortfolioAsync(skillId, portfolioId);

            if (skill == null)
            {
                return new SkillServiceResult<bool>(SkillServiceStatus.SkillNotFound);
            }

            _skillRepo.Delete(skill);
            await _skillRepo.SaveChangesAsync();

            return new SkillServiceResult<bool>(SkillServiceStatus.Success, true);

        }
    }
}
