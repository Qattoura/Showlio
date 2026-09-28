using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;

namespace Showlio.api.Controllers
{
    [Route("api/portfolio/{portfolioId}/certificates")]
    [ApiController]
    public class CertificateController : ControllerBase
    {
        private readonly ICertificateService _certificateService;
        private readonly IValidator<CreateCertificateDto> _createCertificateValidator;
        private readonly IValidator<UpdateCertificateDto> _updateCertificateValidator;

        public CertificateController(
            ICertificateService certificateService,
            IValidator<CreateCertificateDto> createCertificateValidator,
            IValidator<UpdateCertificateDto> updateCertificateValidator)
        {
            _certificateService = certificateService;
            _createCertificateValidator = createCertificateValidator;
            _updateCertificateValidator = updateCertificateValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int portfolioId)
        {
            var result =
                await _certificateService
                    .GetAllCertificatesForPortfolioAsync(portfolioId);

            if (result.Status ==
                Enums.CertificateServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            return Ok(
                result.Data!
                    .Select(c => c.ToDto())
                    .ToList());
        }

        [HttpGet("{certificateId}")]
        [Authorize]
        public async Task<IActionResult> Get(
            int certificateId,
            int portfolioId)
        {
            var certificate =
                await _certificateService.GetCertificateAsync(
                    certificateId,
                    portfolioId);

            if (certificate.Status ==
                Enums.CertificateServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (certificate.Status ==
                Enums.CertificateServiceStatus.CertificateNotFound)
            {
                return NotFound();
            }

            return Ok(certificate.Data!.ToDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(
            CreateCertificateDto createDto,
            int portfolioId)
        {
            var validationResult =
                await _createCertificateValidator
                    .ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var certificate =
                await _certificateService.CreateAsync(
                    createDto,
                    portfolioId);

            if (certificate.Status ==
                Enums.CertificateServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (certificate.Status ==
                Enums.CertificateServiceStatus.CertificateLimitReached)
            {
                return Conflict("Certificate Limit Reached");
            }

            return Ok(certificate.Data!.ToDto());
        }

        [HttpPut("{certificateId}")]
        [Authorize]
        public async Task<IActionResult> Update(
            UpdateCertificateDto updateDto,
            int certificateId,
            int portfolioId)
        {
            var validationResult =
                await _updateCertificateValidator
                    .ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var certificate =
                await _certificateService.UpdateAsync(
                    updateDto,
                    certificateId,
                    portfolioId);

            if (certificate.Status ==
                Enums.CertificateServiceStatus.CertificateNotFound)
            {
                return NotFound();
            }

            if (certificate.Status ==
                Enums.CertificateServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            return Ok(certificate.Data!.ToDto());
        }

        [HttpDelete("{certificateId}")]
        [Authorize]
        public async Task<IActionResult> Delete(
            int certificateId,
            int portfolioId)
        {
            var deleted =
                await _certificateService.DeleteAsync(
                    certificateId,
                    portfolioId);

            if (deleted.Status ==
                Enums.CertificateServiceStatus.CertificateNotFound)
            {
                return NotFound();
            }

            if (deleted.Status ==
                Enums.CertificateServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            return NoContent();
        }
    }
}
