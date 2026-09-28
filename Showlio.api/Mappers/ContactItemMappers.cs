using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class ContactItemMappers
    {
        public static ContactItemDto ToDto(
            this ContactItem contactItem)
        {
            return new ContactItemDto
            {
                Id = contactItem.Id,
                PortfolioId = contactItem.PortfolioId,
                Label = contactItem.Label,
                Value = contactItem.Value,
                DisplayOrder = contactItem.DisplayOrder
            };
        }

        public static ContactItem ToEntity(
            this CreateContactItemDto dto,
            int portfolioId)
        {
            return new ContactItem
            {
                PortfolioId = portfolioId,
                Label = dto.Label,
                Value = dto.Value,
                DisplayOrder = dto.DisplayOrder
            };
        }

        public static void ApplyUpdate(
            this ContactItem contactItem,
            UpdateContactItemDto dto)
        {
            contactItem.Label = dto.Label;
            contactItem.Value = dto.Value;
            contactItem.DisplayOrder = dto.DisplayOrder;
        }
    }
}
