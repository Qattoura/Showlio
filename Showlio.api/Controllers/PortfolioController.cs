using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Showlio.api.Dtos;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Models;

namespace Showlio.api.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioService _portfolioService;
        private readonly IValidator<CreatePortfolioDto> _createPortfolioValidator;
        private readonly IValidator<UpdatePortfolioDto> _updatePortfolioValidator;
        public PortfolioController(IPortfolioService portfolioService,
            IValidator<CreatePortfolioDto> createPortfolioValidator,
            IValidator<UpdatePortfolioDto> updatePortfolioValidator) 
        {
            _portfolioService = portfolioService;
            _createPortfolioValidator = createPortfolioValidator;
            _updatePortfolioValidator = updatePortfolioValidator;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var portfolio = await _portfolioService.GetMyPortfolioAsync();

            if (portfolio == null) 
            {
                return NotFound();
            }

            return Ok(portfolio.ToDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreatePortfolioDto createDto) 
        {
            var validationResult = await _createPortfolioValidator.ValidateAsync(createDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var portfolio = await _portfolioService.CreateAsync(createDto);

            if (portfolio == null) 
            {
                return Conflict("user already has a portfolio");
            }

            return Ok(portfolio.ToDto());
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdatePortfolioDto updateDto) 
        {
            var validationResult = await _updatePortfolioValidator.ValidateAsync(updateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var portfolio = await _portfolioService.UpdateAsync(updateDto);

            if (portfolio == null)
            {
                return NotFound();
            }

            return Ok(portfolio.ToDto());

        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete() 
        {
            var deleted = await _portfolioService.DeleteAsync();

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
