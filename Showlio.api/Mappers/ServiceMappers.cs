using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class ServiceMappers
    {
        public static ServiceDto ToDto(this Service service)
        {
            return new ServiceDto
            {
                Id = service.Id,
                PortfolioId = service.PortfolioId,
                Title = service.Title,
                Description = service.Description,
                Link = service.Link,
                DisplayOrder = service.DisplayOrder
            };
        }

        public static Service ToEntity(
            this CreateServiceDto dto,
            int portfolioId)
        {
            return new Service
            {
                PortfolioId = portfolioId,
                Title = dto.Title,
                Description = dto.Description,
                Link = dto.Link,
                DisplayOrder = dto.DisplayOrder
            };
        }

        public static void ApplyUpdate(
            this Service service,
            UpdateServiceDto dto)
        {
            service.Title = dto.Title;
            service.Description = dto.Description;
            service.Link = dto.Link;
            service.DisplayOrder = dto.DisplayOrder;
        }
    }
}
