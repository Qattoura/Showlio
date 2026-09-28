using Showlio.api.Dtos;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Interfaces.IServices
{
    public interface IContactItemService
    {
        Task<ContactItemServiceResult<IEnumerable<ContactItem>>>
            GetAllContactItemsForPortfolioAsync(
                int portfolioId);

        Task<ContactItemServiceResult<ContactItem>>
            GetContactItemAsync(
                int contactItemId,
                int portfolioId);

        Task<ContactItemServiceResult<ContactItem>>
            CreateAsync(
                CreateContactItemDto createDto,
                int portfolioId);

        Task<ContactItemServiceResult<ContactItem>>
            UpdateAsync(
                UpdateContactItemDto updateDto,
                int contactItemId,
                int portfolioId);

        Task<ContactItemServiceResult<bool>>
            DeleteAsync(
                int contactItemId,
                int portfolioId);
    }
}
