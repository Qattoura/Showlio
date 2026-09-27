using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Models;

namespace Showlio.api.Controllers
{
    [Route("api/portfolio/{portfolioId}/skills")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;
        private readonly IValidator<CreateSkillDto> _createSkillValidator;
        private readonly IValidator<UpdateSkillDto> _updateSkillValidator;

        public SkillController(ISkillService skillService,
            IValidator<CreateSkillDto> createSkillValidator,
            IValidator<UpdateSkillDto> updateSkillValidator) 
        {
            _skillService = skillService;
            _createSkillValidator = createSkillValidator;
            _updateSkillValidator = updateSkillValidator;

        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int portfolioId)
        {
            var result = await _skillService.GetAllSkillsForPortfolioAsync(portfolioId);

            if (result.Status == Enums.SkillServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return Ok(result.Data!.Select(s => s.ToDto()).ToList());

        }

        [HttpGet("{skillId}")]
        [Authorize]
        public async Task<IActionResult> Get(int skillId, int portfolioId)
        {
            var skill = await _skillService.GetSkillAsync(skillId, portfolioId);

            if (skill.Status == Enums.SkillServiceStatus.PortfolioNotOwned) 
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            if (skill.Status == Enums.SkillServiceStatus.SkillNotFound)
            {
                return NotFound();
            }


            return Ok(skill.Data.ToDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateSkillDto createDto, int portfolioId)
        {
            var validationResult = await _createSkillValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var skill = await _skillService.CreateAsync(createDto, portfolioId);

            if (skill.Status == Enums.SkillServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            if (skill.Status == Enums.SkillServiceStatus.SkillLimitReached)
            {
                return Conflict("Skill Limit Reached");
            }

            return Ok(skill.Data.ToDto());
        }

        [HttpPut("{skillId}")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateSkillDto updateDto, int skillId, int portfolioId)
        {
            var validationResult = await _updateSkillValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var skill = await _skillService.UpdateAsync(updateDto, skillId, portfolioId);

            if (skill.Status == Enums.SkillServiceStatus.SkillNotFound)
            {
                return NotFound();
            }



            if (skill.Status == Enums.SkillServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return Ok(skill.Data.ToDto());

        }

        [HttpDelete("{skillId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int skillId, int portfolioId)
        {
            var deleted = await _skillService.DeleteAsync(skillId, portfolioId);

            if (deleted.Status == Enums.SkillServiceStatus.SkillNotFound)
            {
                return NotFound();
            }

            if (deleted.Status == Enums.SkillServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return NoContent();
        }




    }
}
