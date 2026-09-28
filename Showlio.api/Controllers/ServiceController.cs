using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;

namespace Showlio.api.Controllers
{
    [Route("api/portfolio/{portfolioId}/services")]
    [ApiController]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        private readonly IValidator<CreateServiceDto>
            _createServiceValidator;

        private readonly IValidator<UpdateServiceDto>
            _updateServiceValidator;

        public ServiceController(
            IServiceService serviceService,
            IValidator<CreateServiceDto> createServiceValidator,
            IValidator<UpdateServiceDto> updateServiceValidator)
        {
            _serviceService = serviceService;
            _createServiceValidator = createServiceValidator;
            _updateServiceValidator = updateServiceValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int portfolioId)
        {
            var result =
                await _serviceService
                    .GetAllServicesForPortfolioAsync(portfolioId);

            if (result.Status ==
                ServiceServiceStatus.PortfolioNotOwned)
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

        [HttpGet("{serviceId}")]
        [Authorize]
        public async Task<IActionResult> Get(int serviceId, int portfolioId)
        {
            var result =
                await _serviceService
                    .GetServiceAsync(
                        serviceId,
                        portfolioId);

            if (result.Status ==
                ServiceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                ServiceServiceStatus.ServiceNotFound)
            {
                return NotFound();
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateServiceDto createDto, int portfolioId)
        {
            var validationResult =
                await _createServiceValidator
                    .ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result =
                await _serviceService
                    .CreateAsync(
                        createDto,
                        portfolioId);

            if (result.Status ==
                ServiceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                ServiceServiceStatus.ServiceLimitReached)
            {
                return Conflict("Service Limit Reached");
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpPut("{serviceId}")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateServiceDto updateDto,int serviceId,int portfolioId)
        {
            var validationResult =
                await _updateServiceValidator
                    .ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result =
                await _serviceService
                    .UpdateAsync(
                        updateDto,
                        serviceId,
                        portfolioId);

            if (result.Status ==
                ServiceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                ServiceServiceStatus.ServiceNotFound)
            {
                return NotFound();
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpDelete("{serviceId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int serviceId, int portfolioId)
        {
            var result =
                await _serviceService
                    .DeleteAsync(
                        serviceId,
                        portfolioId);

            if (result.Status ==
                ServiceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                ServiceServiceStatus.ServiceNotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
