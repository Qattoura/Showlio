using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Results;
using Showlio.api.validators.Template;

namespace Showlio.api.Controllers
{
    [Route("api/template")]
    [ApiController]
    public class TemplateController : ControllerBase
    {
        private readonly ITemplateService _templateService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IValidator<CreateTemplateDto> _createTemplateValidator;
        private readonly IValidator<UpdateTemplateDto> _updateTemplateValidator;

        public TemplateController(
            ITemplateService templateService,
            ICurrentUserService currentUserService,
            IValidator<CreateTemplateDto> createTemplateValidator,
            IValidator<UpdateTemplateDto> updateTemplateValidator)
        {
            _templateService = templateService;
            _currentUserService = currentUserService;
            _createTemplateValidator = createTemplateValidator;
            _updateTemplateValidator = updateTemplateValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetTemplates()
        {
            var result = await _templateService.GetTemplatesAsync();

            if (result.Status == TemplateServiceStatus.TemplateNotFound)
            {
                return NotFound();
            }

            if (_currentUserService.Role == "Admin")
            {
                return Ok(result.Data!.Select(t => t.ToAdminDto()));
            }

            return Ok(result.Data!.Select(t => t.ToDto()));
        }

        [HttpGet("{templateid}")]
        [Authorize]
        public async Task<IActionResult> GetTemplateById(int templateid)
        {
            var result =
                await _templateService.GetTemplateByIdAsync(templateid);

            if (result.Status == TemplateServiceStatus.TemplateNotFound)
            {
                return NotFound();
            }

            if (_currentUserService.Role == "Admin")
            {
                return Ok(result.Data!.ToAdminDto());
            }

            return Ok(result.Data!.ToDto());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateTemplate(
            CreateTemplateDto createDto)
        {
            var validationResult =
                await _createTemplateValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result =
                await _templateService.CreateTemplateAsync(createDto);

            if (result.Status == TemplateServiceStatus.DuplicateTitle)
            {
                return Conflict("A template with this title already exists");
            }

            return Ok(result.Data!.ToAdminDto());
        }

        [HttpPut("{templateid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTemplate(UpdateTemplateDto updateDto,int templateid)
        {
            var validationResult =
                await _updateTemplateValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result =
                await _templateService.UpdateTemplateAsync(updateDto, templateid);

            if (result.Status == TemplateServiceStatus.TemplateNotFound)
            {
                return NotFound();
            }

            if (result.Status == TemplateServiceStatus.DuplicateTitle)
            {
                return Conflict("A template with this title already exists");
            }

            return Ok(result.Data!.ToAdminDto());
        }

        [HttpDelete("{templateid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTemplate(int templateid)
        {
            var result =
                await _templateService.DeleteTemplateAsync(templateid);

            if (result == TemplateServiceStatus.TemplateNotFound)
            {
                return NotFound();
            }

            if (result == TemplateServiceStatus.TemplateInUse)
            {
                return Conflict(
                    "This template is currently used by one or more portfolios");
            }

            return NoContent();
        }
    }
}