using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;


namespace Showlio.api.Controllers
{
    [Route("api/portfolio/{portfolioId}/contact-items")]
    [ApiController]
    [Authorize]
    public class ContactItemController : ControllerBase
    {
        private readonly IContactItemService _contactItemService;

        private readonly IValidator<CreateContactItemDto>
            _createContactItemValidator;

        private readonly IValidator<UpdateContactItemDto>
            _updateContactItemValidator;

        public ContactItemController(
            IContactItemService contactItemService,
            IValidator<CreateContactItemDto>
                createContactItemValidator,
            IValidator<UpdateContactItemDto>
                updateContactItemValidator)
        {
            _contactItemService = contactItemService;
            _createContactItemValidator =
                createContactItemValidator;
            _updateContactItemValidator =
                updateContactItemValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int portfolioId)
        {
            var result =
                await _contactItemService
                    .GetAllContactItemsForPortfolioAsync(
                        portfolioId);

            if (result.Status ==
                ContactItemServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            return Ok(
                result.Data!
                    .Select(x => x.ToDto())
                    .ToList());
        }

        [HttpGet("{contactItemId}")]
        [Authorize]
        public async Task<IActionResult> Get(int contactItemId, int portfolioId)
        {
            var result =
                await _contactItemService
                    .GetContactItemAsync(
                        contactItemId,
                        portfolioId);

            if (result.Status ==
                ContactItemServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                ContactItemServiceStatus.ContactItemNotFound)
            {
                return NotFound();
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateContactItemDto createDto, int portfolioId)
        {
            var validationResult =
                await _createContactItemValidator
                    .ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result =
                await _contactItemService
                    .CreateAsync(
                        createDto,
                        portfolioId);

            if (result.Status ==
                ContactItemServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                ContactItemServiceStatus.ContactItemLimitReached)
            {
                return Conflict("ContactItem Limit Reached");
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpPut("{contactItemId}")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateContactItemDto updateDto,int contactItemId,int portfolioId)
        {
            var validationResult =
                await _updateContactItemValidator
                    .ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result =
                await _contactItemService
                    .UpdateAsync(
                        updateDto,
                        contactItemId,
                        portfolioId);

            if (result.Status ==
                ContactItemServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                ContactItemServiceStatus.ContactItemNotFound)
            {
                return NotFound();
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpDelete("{contactItemId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int contactItemId, int portfolioId)
        {
            var result =
                await _contactItemService
                    .DeleteAsync(
                        contactItemId,
                        portfolioId);

            if (result.Status ==
                ContactItemServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                ContactItemServiceStatus.ContactItemNotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
