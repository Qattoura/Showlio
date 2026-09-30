using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Models;
using Showlio.api.Results;
using Showlio.api.Globals;

namespace Showlio.api.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly ICertificateRepository _certificateRepo;
        private readonly IPortfolioAuthorizationService _portfolioAuthoService;

        public CertificateService(
            ICertificateRepository certificateRepository,
            IPortfolioAuthorizationService portfolioAuthorizationService)
        {
            _certificateRepo = certificateRepository;
            _portfolioAuthoService = portfolioAuthorizationService;
        }

        // Return how many certificates per portfolio
        private async Task<int> GetCertificateCountAsync(int portfolioId)
        {
            return await _certificateRepo.CountCertificatesForPortfolioAsync(portfolioId);
        }

        public async Task<CertificateServiceResult<IEnumerable<Certificate>>>GetAllCertificatesForPortfolioAsync(int portfolioId)
        {
            var isOwner =await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new CertificateServiceResult<IEnumerable<Certificate>>(CertificateServiceStatus.PortfolioNotOwned);
            }

            var certificates =await _certificateRepo.GetAllForPortfolioAsync(portfolioId);

            return new CertificateServiceResult<IEnumerable<Certificate>>(CertificateServiceStatus.Success,certificates);
        }

        public async Task<CertificateServiceResult<Certificate>>GetCertificateAsync(int certificateId,int portfolioId)
        {
            var isOwner =
                await _portfolioAuthoService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new CertificateServiceResult<Certificate>(
                    CertificateServiceStatus.PortfolioNotOwned);
            }

            var certificate =
                await _certificateRepo.GetByIdForPortfolioAsync(
                    certificateId,
                    portfolioId);

            if (certificate == null)
            {
                return new CertificateServiceResult<Certificate>(
                    CertificateServiceStatus.CertificateNotFound);
            }

            return new CertificateServiceResult<Certificate>(
                CertificateServiceStatus.Success,
                certificate);
        }

        public async Task<CertificateServiceResult<Certificate>>CreateAsync(CreateCertificateDto createDto,int portfolioId)
        {
            // User owns the portfolio
            var isOwner =
                await _portfolioAuthoService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new CertificateServiceResult<Certificate>(
                    CertificateServiceStatus.PortfolioNotOwned);
            }

            // Maximum 10 certificates
            if (await GetCertificateCountAsync(portfolioId) >= GlobalConstants.CertificateMax)
            {
                return new CertificateServiceResult<Certificate>(
                    CertificateServiceStatus.CertificateLimitReached);
            }

            var certificate =
                await _certificateRepo.CreateAsync(
                    createDto.ToEntity(portfolioId));

            await _certificateRepo.SaveChangesAsync();

            return new CertificateServiceResult<Certificate>(
                CertificateServiceStatus.Success,
                certificate);
        }

        public async Task<CertificateServiceResult<Certificate>>UpdateAsync(UpdateCertificateDto updateDto,int certificateId,int portfolioId)
        {
            var isOwner =await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new CertificateServiceResult<Certificate>(CertificateServiceStatus.PortfolioNotOwned);
            }

            var certificate =
                await _certificateRepo.GetByIdForPortfolioAsync(certificateId,portfolioId);

            if (certificate == null)
            {
                return new CertificateServiceResult<Certificate>(CertificateServiceStatus.CertificateNotFound);
            }

            certificate.ApplyUpdate(updateDto);

            _certificateRepo.Update(certificate);

            await _certificateRepo.SaveChangesAsync();

            return new CertificateServiceResult<Certificate>(CertificateServiceStatus.Success,certificate);
        }

        public async Task<CertificateServiceResult<bool>>DeleteAsync(int certificateId,int portfolioId)
        {
            var isOwner =
                await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new CertificateServiceResult<bool>(CertificateServiceStatus.PortfolioNotOwned);
            }

            var certificate =
                await _certificateRepo.GetByIdForPortfolioAsync(certificateId,portfolioId);

            if (certificate == null)
            {
                return new CertificateServiceResult<bool>(CertificateServiceStatus.CertificateNotFound);
            }

            _certificateRepo.Delete(certificate);

            await _certificateRepo.SaveChangesAsync();

            return new CertificateServiceResult<bool>(CertificateServiceStatus.Success,true);
        }
    }
}