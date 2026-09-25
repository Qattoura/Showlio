using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Interfaces.IServices
{
    public interface IPortfolioService
    {
        Task<Portfolio?> GetMyPortfolioAsync();
        Task<Portfolio?> CreateAsync(CreatePortfolioDto createDto);
        Task<Portfolio?> UpdateAsync(UpdatePortfolioDto updateDto);
        Task<bool> DeleteAsync();
    }
}
