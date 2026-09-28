using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class CertificateMappers
    {
        public static CertificateDto ToDto(this Certificate certificate)
        {
            return new CertificateDto
            {
                Id = certificate.Id,
                PortfolioId = certificate.PortfolioId,
                Title = certificate.Title,
                Description = certificate.Description,
                Image = certificate.Image,
                CertificateLink = certificate.CertificateLink,
                DisplayOrder = certificate.DisplayOrder
            };
        }

        public static Certificate ToEntity(
            this CreateCertificateDto dto,
            int portfolioId)
        {
            return new Certificate
            {
                PortfolioId = portfolioId,
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image,
                CertificateLink = dto.CertificateLink,
                DisplayOrder = dto.DisplayOrder
            };
        }

        public static void ApplyUpdate(
            this Certificate certificate,
            UpdateCertificateDto dto)
        {
            certificate.Title = dto.Title;
            certificate.Description = dto.Description;
            certificate.Image = dto.Image;
            certificate.CertificateLink = dto.CertificateLink;
            certificate.DisplayOrder = dto.DisplayOrder;
        }
    }
}