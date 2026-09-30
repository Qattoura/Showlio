using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Models;
using Showlio.api.Results;
using Showlio.api.Mappers;
using Showlio.api.Globals;

namespace Showlio.api.Services
{
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepo;
        private readonly IPortfolioAuthorizationService
            _portfolioAuthorizationService;


        public EducationService(
            IEducationRepository educationRepo,
            IPortfolioAuthorizationService portfolioAuthorizationService)
        {
            _educationRepo = educationRepo;
            _portfolioAuthorizationService =
                portfolioAuthorizationService;
        }

        public async Task<EducationServiceResult<IEnumerable<Education>>>
            GetAllEducationsForPortfolioAsync(int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new EducationServiceResult<IEnumerable<Education>>(
                    EducationServiceStatus.PortfolioNotOwned);
            }

            var educations =
                await _educationRepo.GetAllForPortfolioAsync(portfolioId);

            return new EducationServiceResult<IEnumerable<Education>>(
                EducationServiceStatus.Success,
                educations);
        }

        public async Task<EducationServiceResult<Education>>
            GetEducationAsync(
                int educationId,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new EducationServiceResult<Education>(
                    EducationServiceStatus.PortfolioNotOwned);
            }

            var education =
                await _educationRepo.GetByIdForPortfolioAsync(
                    educationId,
                    portfolioId);

            if (education == null)
            {
                return new EducationServiceResult<Education>(
                    EducationServiceStatus.EducationNotFound);
            }

            return new EducationServiceResult<Education>(
                EducationServiceStatus.Success,
                education);
        }

        public async Task<EducationServiceResult<Education>>
            CreateAsync(
                CreateEducationDto createDto,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new EducationServiceResult<Education>(
                    EducationServiceStatus.PortfolioNotOwned);
            }

            var educationCount =
                await GetEducationCountAsync(portfolioId);

            if (educationCount >= GlobalConstants.EducationMax)
            {
                return new EducationServiceResult<Education>(
                    EducationServiceStatus.EducationLimitReached);
            }

            var education =
                createDto.ToEntity(portfolioId);

            var createdEducation =
                await _educationRepo.CreateAsync(education);

            await _educationRepo.SaveChangesAsync();

            return new EducationServiceResult<Education>(
                EducationServiceStatus.Success,
                createdEducation);
        }

        public async Task<EducationServiceResult<Education>>
            UpdateAsync(
                UpdateEducationDto updateDto,
                int educationId,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new EducationServiceResult<Education>(
                    EducationServiceStatus.PortfolioNotOwned);
            }

            var education =
                await _educationRepo.GetByIdForPortfolioAsync(
                    educationId,
                    portfolioId);

            if (education == null)
            {
                return new EducationServiceResult<Education>(
                    EducationServiceStatus.EducationNotFound);
            }

            education.ApplyUpdate(updateDto);

            _educationRepo.Update(education);

            await _educationRepo.SaveChangesAsync();

            return new EducationServiceResult<Education>(
                EducationServiceStatus.Success,
                education);
        }

        public async Task<EducationServiceResult<bool>>
            DeleteAsync(
                int educationId,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new EducationServiceResult<bool>(
                    EducationServiceStatus.PortfolioNotOwned);
            }

            var education =
                await _educationRepo.GetByIdForPortfolioAsync(
                    educationId,
                    portfolioId);

            if (education == null)
            {
                return new EducationServiceResult<bool>(
                    EducationServiceStatus.EducationNotFound);
            }

            _educationRepo.Delete(education);

            await _educationRepo.SaveChangesAsync();

            return new EducationServiceResult<bool>(
                EducationServiceStatus.Success,
                true);
        }

        private async Task<int> GetEducationCountAsync(
            int portfolioId)
        {
            return await _educationRepo
                .CountEducationsForPortfolioAsync(portfolioId);
        }
    }
}
