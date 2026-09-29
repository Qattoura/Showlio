using Microsoft.AspNetCore.Mvc;
using Showlio.api.Interfaces.IServices;

namespace Showlio.api.Controllers
{

    [ApiController]
    [Route("api/public")]
    public class PublicPortfolioController : ControllerBase
    {
        private readonly IPublicPortfolioService _publicPortfolioService;

        public PublicPortfolioController(
            IPublicPortfolioService publicPortfolioService)
        {
            _publicPortfolioService = publicPortfolioService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetPortfolio(string username)
        {
            var portfolio = await _publicPortfolioService.GetPublishedPortfolioByUsernameAsync(username);

            if (portfolio == null)
            {
                return NotFound();
            }

            return Ok(portfolio);
        }
    }

}
