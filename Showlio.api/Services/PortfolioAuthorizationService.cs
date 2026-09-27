using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;

namespace Showlio.api.Services
{
    public class PortfolioAuthorizationService : IPortfolioAuthorizationService
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IPortfolioRepository _portfolioRepo;

        public PortfolioAuthorizationService(
            ICurrentUserService currentUserService,
            IPortfolioRepository portfolioRepo)
        {
            _currentUserService = currentUserService;
            _portfolioRepo = portfolioRepo;
        }

        public async Task<bool> IsOwnedByCurrentUserAsync(int portfolioId)
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
            {
                return false;
            }

            return await _portfolioRepo.IsOwnedByUserAsync(
                portfolioId,
                userId.Value);
        }
    }
}
