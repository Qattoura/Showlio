using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Services;

namespace Showlio.api.Controllers
{
    [Route("api/portfolio/{portfolioId}/projects")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IValidator<CreateProjectDto> _createProjectValidator;
        private readonly IValidator<UpdateProjectDto> _updateProjectValidator;
        public ProjectController(IProjectService projectService,
            IValidator<CreateProjectDto> createvalidator,
            IValidator<UpdateProjectDto> updatevalidator)
        {
            _projectService = projectService;
            _createProjectValidator = createvalidator;
            _updateProjectValidator = updatevalidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll(int portfolioId)
        {
            var result = await _projectService.GetAllProjectsForPortfolioAsync(portfolioId);

            if (result.Status == Enums.ProjectServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return Ok(result.Data!.Select(p => p.ToDto()).ToList());

        }


        [HttpGet("{projectId}")]
        [Authorize]
        public async Task<IActionResult> Get(int projectId, int portfolioId)
        {
            var project = await _projectService.GetProjectAsync(projectId, portfolioId);

            if (project.Status == Enums.ProjectServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            if (project.Status == Enums.ProjectServiceStatus.ProjectNotFound)
            {
                return NotFound();
            }


            return Ok(project.Data.ToDto());
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateProjectDto createDto, int portfolioId)
        {

            var validationResult = await _createProjectValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var project = await _projectService.CreateAsync(createDto, portfolioId);

            if (project.Status == Enums.ProjectServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            if (project.Status == Enums.ProjectServiceStatus.ProjectNotFound)
            {
                return Conflict("Skill Limit Reached");
            }

            return Ok(project.Data.ToDto());
        }


        [HttpPut("{projectId}")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateProjectDto updateDto, int projectId, int portfolioId)
        {
            var validationResult = await _updateProjectValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var project = await _projectService.UpdateAsync(updateDto, projectId, portfolioId);

            if (project.Status == Enums.ProjectServiceStatus.ProjectNotFound)
            {
                return NotFound();
            }



            if (project.Status == Enums.ProjectServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return Ok(project.Data.ToDto());

        }


        [HttpDelete("{projectId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int projectId, int portfolioId)
        {
            var deleted = await _projectService.DeleteAsync(projectId, portfolioId);

            if (deleted.Status == Enums.ProjectServiceStatus.ProjectNotFound)
            {
                return NotFound();
            }

            if (deleted.Status == Enums.ProjectServiceStatus.PortfolioNotOwned)
            {
                return StatusCode(403, "User doesn't own the portfolio");
            }

            return NoContent();
        }

    }
}
