namespace Showlio.api.Interfaces.IServices
{
    public interface IPortfolioAuthorizationService
    {
        Task<bool> IsOwnedByCurrentUserAsync(int portfolioId);
    }
}
