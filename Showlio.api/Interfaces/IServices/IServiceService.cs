using Showlio.api.Dtos;
using Showlio.api.Models;
using Showlio.api.Results;


namespace Showlio.api.Interfaces.IServices
{
    public interface IServiceService
    {
        Task<ServiceServiceResult<IEnumerable<Service>>>GetAllServicesForPortfolioAsync(int portfolioId);

        Task<ServiceServiceResult<Service>>GetServiceAsync(int serviceId,int portfolioId);

        Task<ServiceServiceResult<Service>>CreateAsync(CreateServiceDto createDto,int portfolioId);

        Task<ServiceServiceResult<Service>>UpdateAsync(UpdateServiceDto updateDto,int serviceId,int portfolioId);

        Task<ServiceServiceResult<bool>>DeleteAsync(int serviceId,int portfolioId);
    }
}
