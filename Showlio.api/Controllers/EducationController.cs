using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;

namespace Showlio.api.Controllers
{
    [Route("api/portfolio/{portfolioId}/educations")]
    [ApiController]
    [Authorize]
    public class EducationController : ControllerBase
    {
        private readonly IEducationService _educationService;

        private readonly IValidator<CreateEducationDto>
            _createEducationValidator;

        private readonly IValidator<UpdateEducationDto>
            _updateEducationValidator;

        public EducationController(
            IEducationService educationService,
            IValidator<CreateEducationDto> createEducationValidator,
            IValidator<UpdateEducationDto> updateEducationValidator)
        {
            _educationService = educationService;
            _createEducationValidator = createEducationValidator;
            _updateEducationValidator = updateEducationValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int portfolioId)
        {
            var result =
                await _educationService
                    .GetAllEducationsForPortfolioAsync(portfolioId);

            if (result.Status ==
                EducationServiceStatus.PortfolioNotOwned)
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

        [HttpGet("{educationId}")]
        [Authorize]
        public async Task<IActionResult> Get(int educationId, int portfolioId)
        {
            var result =
                await _educationService
                    .GetEducationAsync(
                        educationId,
                        portfolioId);

            if (result.Status ==
                EducationServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                EducationServiceStatus.EducationNotFound)
            {
                return NotFound();
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(
            int portfolioId,
            CreateEducationDto createDto)
        {
            var validationResult =
                await _createEducationValidator
                    .ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result =
                await _educationService
                    .CreateAsync(
                        createDto,
                        portfolioId);

            if (result.Status ==
                EducationServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                EducationServiceStatus.EducationLimitReached)
            {
                return Conflict("Education Limit Reached");
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpPut("{educationId}")]
        [Authorize]
        public async Task<IActionResult>Update(UpdateEducationDto updateDto, int educationId, int portfolioId)
        {
            var validationResult =
                await _updateEducationValidator
                    .ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result =
                await _educationService
                    .UpdateAsync(
                        updateDto,
                        educationId,
                        portfolioId);

            if (result.Status ==
                EducationServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                EducationServiceStatus.EducationNotFound)
            {
                return NotFound();
            }

            return Ok(result.Data!.ToDto());
        }


        [HttpDelete("{educationId}")]
        [Authorize]
        public async Task<IActionResult>Delete(int educationId, int portfolioId)
        {
            var result =
                await _educationService
                    .DeleteAsync(
                        educationId,
                        portfolioId);

            if (result.Status ==
                EducationServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(
                    403,
                    "User doesn't own the portfolio");
            }

            if (result.Status ==
                EducationServiceStatus.EducationNotFound)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
