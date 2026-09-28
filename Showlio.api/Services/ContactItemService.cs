using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Models;
using Showlio.api.Results;
using Showlio.api.Mappers;

namespace Showlio.api.Services
{
    public class ContactItemService : IContactItemService
    {
        private readonly IContactItemRepository _contactItemRepo;

        private readonly IPortfolioAuthorizationService
            _portfolioAuthorizationService;

        private const int MaxContactItemsPerPortfolio = 10;

        public ContactItemService(
            IContactItemRepository contactItemRepo,
            IPortfolioAuthorizationService portfolioAuthorizationService)
        {
            _contactItemRepo = contactItemRepo;
            _portfolioAuthorizationService =
                portfolioAuthorizationService;
        }

        public async Task<
            ContactItemServiceResult<IEnumerable<ContactItem>>>
            GetAllContactItemsForPortfolioAsync(
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ContactItemServiceResult<
                    IEnumerable<ContactItem>>(
                    ContactItemServiceStatus.PortfolioNotOwned);
            }

            var contactItems =
                await _contactItemRepo
                    .GetAllForPortfolioAsync(portfolioId);

            return new ContactItemServiceResult<
                IEnumerable<ContactItem>>(
                ContactItemServiceStatus.Success,
                contactItems);
        }

        public async Task<
            ContactItemServiceResult<ContactItem>>
            GetContactItemAsync(
                int contactItemId,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ContactItemServiceResult<ContactItem>(
                    ContactItemServiceStatus.PortfolioNotOwned);
            }

            var contactItem =
                await _contactItemRepo
                    .GetByIdForPortfolioAsync(
                        contactItemId,
                        portfolioId);

            if (contactItem == null)
            {
                return new ContactItemServiceResult<ContactItem>(
                    ContactItemServiceStatus.ContactItemNotFound);
            }

            return new ContactItemServiceResult<ContactItem>(
                ContactItemServiceStatus.Success,
                contactItem);
        }

        public async Task<
            ContactItemServiceResult<ContactItem>>
            CreateAsync(
                CreateContactItemDto createDto,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ContactItemServiceResult<ContactItem>(
                    ContactItemServiceStatus.PortfolioNotOwned);
            }

            var contactItemCount =
                await GetContactItemCountAsync(portfolioId);

            if (contactItemCount >= MaxContactItemsPerPortfolio)
            {
                return new ContactItemServiceResult<ContactItem>(
                    ContactItemServiceStatus.ContactItemLimitReached);
            }

            var contactItem =
                createDto.ToEntity(portfolioId);

            var createdContactItem =
                await _contactItemRepo.CreateAsync(contactItem);

            await _contactItemRepo.SaveChangesAsync();

            return new ContactItemServiceResult<ContactItem>(
                ContactItemServiceStatus.Success,
                createdContactItem);
        }

        public async Task<
            ContactItemServiceResult<ContactItem>>
            UpdateAsync(
                UpdateContactItemDto updateDto,
                int contactItemId,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ContactItemServiceResult<ContactItem>(
                    ContactItemServiceStatus.PortfolioNotOwned);
            }

            var contactItem =
                await _contactItemRepo
                    .GetByIdForPortfolioAsync(
                        contactItemId,
                        portfolioId);

            if (contactItem == null)
            {
                return new ContactItemServiceResult<ContactItem>(
                    ContactItemServiceStatus.ContactItemNotFound);
            }

            contactItem.ApplyUpdate(updateDto);

            _contactItemRepo.Update(contactItem);

            await _contactItemRepo.SaveChangesAsync();

            return new ContactItemServiceResult<ContactItem>(
                ContactItemServiceStatus.Success,
                contactItem);
        }

        public async Task<
            ContactItemServiceResult<bool>>
            DeleteAsync(
                int contactItemId,
                int portfolioId)
        {
            var isOwned =
                await _portfolioAuthorizationService
                    .IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwned)
            {
                return new ContactItemServiceResult<bool>(
                    ContactItemServiceStatus.PortfolioNotOwned);
            }

            var contactItem =
                await _contactItemRepo
                    .GetByIdForPortfolioAsync(
                        contactItemId,
                        portfolioId);

            if (contactItem == null)
            {
                return new ContactItemServiceResult<bool>(
                    ContactItemServiceStatus.ContactItemNotFound);
            }

            _contactItemRepo.Delete(contactItem);

            await _contactItemRepo.SaveChangesAsync();

            return new ContactItemServiceResult<bool>(
                ContactItemServiceStatus.Success,
                true);
        }

        private async Task<int> GetContactItemCountAsync(
            int portfolioId)
        {
            return await _contactItemRepo
                .CountContactItemsForPortfolioAsync(portfolioId);
        }
    }
}
