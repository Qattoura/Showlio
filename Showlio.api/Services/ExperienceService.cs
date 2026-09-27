using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepo;
        private readonly IPortfolioAuthorizationService _portfolioAuthoService;

        public ExperienceService(
            IExperienceRepository experienceRepository,
            IPortfolioAuthorizationService portfolioAuthorizationService)
        {
            _experienceRepo = experienceRepository;
            _portfolioAuthoService = portfolioAuthorizationService;
        }

        // Return how many experiences per portfolio
        private async Task<int> GetExperienceCountAsync(int portfolioId)
        {
            return await _experienceRepo.CountExperiencesForPortfolioAsync(portfolioId);
        }

        public async Task<ExperienceServiceResult<IEnumerable<Experience>>>
            GetAllExperiencesForPortfolioAsync(int portfolioId)
        {
            var isOwner =
                await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ExperienceServiceResult<IEnumerable<Experience>>(
                    ExperienceServiceStatus.PortfolioNotOwned);
            }

            var experiences =
                await _experienceRepo.GetAllForPortfolioAsync(portfolioId);

            return new ExperienceServiceResult<IEnumerable<Experience>>(
                ExperienceServiceStatus.Success,
                experiences);
        }

        public async Task<ExperienceServiceResult<Experience>> GetExperienceAsync(
            int experienceId,
            int portfolioId)
        {
            var isOwner =
                await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ExperienceServiceResult<Experience>(
                    ExperienceServiceStatus.PortfolioNotOwned);
            }

            var experience =
                await _experienceRepo.GetByIdForPortfolioAsync(
                    experienceId,
                    portfolioId);

            if (experience == null)
            {
                return new ExperienceServiceResult<Experience>(
                    ExperienceServiceStatus.ExperienceNotFound);
            }

            return new ExperienceServiceResult<Experience>(
                ExperienceServiceStatus.Success,
                experience);
        }

        public async Task<ExperienceServiceResult<Experience>> CreateAsync(CreateExperienceDto createDto,int portfolioId)
        {
            // User owns the portfolio
            var isOwner =
                await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ExperienceServiceResult<Experience>(
                    ExperienceServiceStatus.PortfolioNotOwned);
            }

            // Maximum 10 experiences
            if (await GetExperienceCountAsync(portfolioId) >= 10)
            {
                return new ExperienceServiceResult<Experience>(
                    ExperienceServiceStatus.ExperienceLimitReached);
            }

            var experience =
                await _experienceRepo.CreateAsync(
                    createDto.ToEntity(portfolioId));

            await _experienceRepo.SaveChangesAsync();

            return new ExperienceServiceResult<Experience>(
                ExperienceServiceStatus.Success,
                experience);
        }

        public async Task<ExperienceServiceResult<Experience>> UpdateAsync(UpdateExperienceDto updateDto,int experienceId,int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ExperienceServiceResult<Experience>(ExperienceServiceStatus.PortfolioNotOwned);
            }

            var experience =
                await _experienceRepo.GetByIdForPortfolioAsync(experienceId,portfolioId);

            if (experience == null)
            {
                return new ExperienceServiceResult<Experience>(ExperienceServiceStatus.ExperienceNotFound);
            }

            experience.ApplyUpdate(updateDto);

            _experienceRepo.Update(experience);

            await _experienceRepo.SaveChangesAsync();

            return new ExperienceServiceResult<Experience>(ExperienceServiceStatus.Success,experience);
        }

        public async Task<ExperienceServiceResult<bool>> DeleteAsync(int experienceId,int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ExperienceServiceResult<bool>(ExperienceServiceStatus.PortfolioNotOwned);
            }

            var experience =
                await _experienceRepo.GetByIdForPortfolioAsync(experienceId,portfolioId);

            if (experience == null)
            {
                return new ExperienceServiceResult<bool>(ExperienceServiceStatus.ExperienceNotFound);
            }

            _experienceRepo.Delete(experience);

            await _experienceRepo.SaveChangesAsync();

            return new ExperienceServiceResult<bool>(ExperienceServiceStatus.Success,true);
        }
    }
}