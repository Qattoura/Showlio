using Showlio.api.Dtos;

namespace Showlio.api.Interfaces.IServices
{
    public interface IPublicPortfolioService
    {
        Task<PublicPortfolioDto?> GetPublishedPortfolioByUsernameAsync(string username);
    }
}
