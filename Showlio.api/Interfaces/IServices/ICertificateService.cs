using Showlio.api.Dtos;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Interfaces.IServices
{
    public interface ICertificateService
    {
        Task<CertificateServiceResult<IEnumerable<Certificate>>>GetAllCertificatesForPortfolioAsync(int portfolioId);

        Task<CertificateServiceResult<Certificate>> GetCertificateAsync(int certificateId,int portfolioId);

        Task<CertificateServiceResult<Certificate>> CreateAsync(CreateCertificateDto createDto,int portfolioId);

        Task<CertificateServiceResult<Certificate>> UpdateAsync(UpdateCertificateDto updateDto,int certificateId,int portfolioId);

        Task<CertificateServiceResult<bool>> DeleteAsync(int certificateId,int portfolioId);
    }
}