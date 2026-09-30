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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepo;

        private readonly IPortfolioAuthorizationService
            _portfolioAuthorizationService;


        public ServiceService(
            IServiceRepository serviceRepo,
            IPortfolioAuthorizationService portfolioAuthorizationService)
        {
            _serviceRepo = serviceRepo;
            _portfolioAuthorizationService =
                portfolioAuthorizationService;
        }

        public async Task<ServiceServiceResult<IEnumerable<Service>>>
            GetAllServicesForPortfolioAsync(int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ServiceServiceResult<IEnumerable<Service>>(
                    ServiceServiceStatus.PortfolioNotOwned);
            }

            var services =
                await _serviceRepo
                    .GetAllForPortfolioAsync(portfolioId);

            return new ServiceServiceResult<IEnumerable<Service>>(
                ServiceServiceStatus.Success,
                services);
        }

        public async Task<ServiceServiceResult<Service>>
            GetServiceAsync(
                int serviceId,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ServiceServiceResult<Service>(
                    ServiceServiceStatus.PortfolioNotOwned);
            }

            var service =
                await _serviceRepo
                    .GetByIdForPortfolioAsync(
                        serviceId,
                        portfolioId);

            if (service == null)
            {
                return new ServiceServiceResult<Service>(
                    ServiceServiceStatus.ServiceNotFound);
            }

            return new ServiceServiceResult<Service>(
                ServiceServiceStatus.Success,
                service);
        }

        public async Task<ServiceServiceResult<Service>>CreateAsync(CreateServiceDto createDto,int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ServiceServiceResult<Service>(
                    ServiceServiceStatus.PortfolioNotOwned);
            }

            var serviceCount =
                await GetServiceCountAsync(portfolioId);

            if (serviceCount >= GlobalConstants.ServiceMax)
            {
                return new ServiceServiceResult<Service>(
                    ServiceServiceStatus.ServiceLimitReached);
            }

            var service =
                createDto.ToEntity(portfolioId);

            var createdService =
                await _serviceRepo.CreateAsync(service);

            await _serviceRepo.SaveChangesAsync();

            return new ServiceServiceResult<Service>(
                ServiceServiceStatus.Success,
                createdService);
        }

        public async Task<ServiceServiceResult<Service>>UpdateAsync(UpdateServiceDto updateDto,int serviceId,int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ServiceServiceResult<Service>(
                    ServiceServiceStatus.PortfolioNotOwned);
            }

            var service =
                await _serviceRepo
                    .GetByIdForPortfolioAsync(
                        serviceId,
                        portfolioId);

            if (service == null)
            {
                return new ServiceServiceResult<Service>(
                    ServiceServiceStatus.ServiceNotFound);
            }

            service.ApplyUpdate(updateDto);

            _serviceRepo.Update(service);

            await _serviceRepo.SaveChangesAsync();

            return new ServiceServiceResult<Service>(
                ServiceServiceStatus.Success,
                service);
        }

        public async Task<ServiceServiceResult<bool>>
            DeleteAsync(
                int serviceId,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ServiceServiceResult<bool>(
                    ServiceServiceStatus.PortfolioNotOwned);
            }

            var service =
                await _serviceRepo
                    .GetByIdForPortfolioAsync(
                        serviceId,
                        portfolioId);

            if (service == null)
            {
                return new ServiceServiceResult<bool>(
                    ServiceServiceStatus.ServiceNotFound);
            }

            _serviceRepo.Delete(service);

            await _serviceRepo.SaveChangesAsync();

            return new ServiceServiceResult<bool>(
                ServiceServiceStatus.Success,
                true);
        }

        private async Task<int> GetServiceCountAsync(
            int portfolioId)
        {
            return await _serviceRepo
                .CountServicesForPortfolioAsync(portfolioId);
        }
    }
}
