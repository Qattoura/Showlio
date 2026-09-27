using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;

namespace Showlio.api.Controllers
{
    [Route("api/portfolio/{portfolioId}/experiences")]
    [ApiController]
    public class ExperienceController : ControllerBase
    {
        private readonly IExperienceService _experienceService;
        private readonly IValidator<CreateExperienceDto> _createExperienceValidator;
        private readonly IValidator<UpdateExperienceDto> _updateExperienceValidator;

        public ExperienceController(
            IExperienceService experienceService,
            IValidator<CreateExperienceDto> createExperienceValidator,
            IValidator<UpdateExperienceDto> updateExperienceValidator)
        {
            _experienceService = experienceService;
            _createExperienceValidator = createExperienceValidator;
            _updateExperienceValidator = updateExperienceValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int portfolioId)
        {
            var result =
                await _experienceService.GetAllExperiencesForPortfolioAsync(
                    portfolioId);

            if (result.Status == Enums.ExperienceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return Ok(
                result.Data!
                    .Select(e => e.ToDto())
                    .ToList());
        }

        [HttpGet("{experienceId}")]
        [Authorize]
        public async Task<IActionResult> Get(
            int experienceId,
            int portfolioId)
        {
            var experience =
                await _experienceService.GetExperienceAsync(
                    experienceId,
                    portfolioId);

            if (experience.Status ==
                Enums.ExperienceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            if (experience.Status ==
                Enums.ExperienceServiceStatus.ExperienceNotFound)
            {
                return NotFound();
            }

            return Ok(experience.Data!.ToDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(
            CreateExperienceDto createDto,
            int portfolioId)
        {
            var validationResult =await _createExperienceValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var experience =
                await _experienceService.CreateAsync(
                    createDto,
                    portfolioId);

            if (experience.Status ==
                Enums.ExperienceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            if (experience.Status ==
                Enums.ExperienceServiceStatus.ExperienceLimitReached)
            {
                return Conflict("Experience Limit Reached");
            }

            return Ok(experience.Data!.ToDto());
        }

        [HttpPut("{experienceId}")]
        [Authorize]
        public async Task<IActionResult> Update(
            UpdateExperienceDto updateDto,
            int experienceId,
            int portfolioId)
        {
            var validationResult =
                await _updateExperienceValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var experience =
                await _experienceService.UpdateAsync(
                    updateDto,
                    experienceId,
                    portfolioId);

            if (experience.Status ==
                Enums.ExperienceServiceStatus.ExperienceNotFound)
            {
                return NotFound();
            }

            if (experience.Status ==
                Enums.ExperienceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return Ok(experience.Data!.ToDto());
        }

        [HttpDelete("{experienceId}")]
        [Authorize]
        public async Task<IActionResult> Delete(
            int experienceId,
            int portfolioId)
        {
            var deleted =
                await _experienceService.DeleteAsync(
                    experienceId,
                    portfolioId);

            if (deleted.Status ==
                Enums.ExperienceServiceStatus.ExperienceNotFound)
            {
                return NotFound();
            }

            if (deleted.Status ==
                Enums.ExperienceServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return NoContent();
        }
    }
}