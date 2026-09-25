using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Showlio.api.Dtos;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Models;
using Showlio.api.Mappers;

namespace Showlio.api.Services
{
    public class PortfolioService : IPortfolioService
    {
        private readonly IPortfolioRepository _portfolioRepo;
        private readonly ICurrentUserService _currentUserService;

        public PortfolioService(IPortfolioRepository portfolioRepo, 
            ICurrentUserService currentUserService) 
        {
            _portfolioRepo = portfolioRepo;
            _currentUserService = currentUserService;
        }


        public async Task<Portfolio?> GetMyPortfolioAsync()
        {
            var userId = _currentUserService.UserId;

            if (userId == null)
                return null;

            return await _portfolioRepo.GetByUserIdAsync(userId.Value);
        }


        public async Task<Portfolio?> CreateAsync(CreatePortfolioDto createDto)
        {


            var userId = _currentUserService.UserId;
            var alreadyExists = await _portfolioRepo.ExistsByUserIdAsync(userId.Value);

            if (alreadyExists)
                return null;

            var portfolioModel = new Portfolio
            {
                UserId = userId.Value,
                PortfolioName = createDto.PortfolioName,
                Name = createDto.Name,
                JobTitle = createDto.JobTitle,
                Description = createDto.Description,
                TemplateId = createDto.TemplateId
            };



            var portfolio = await _portfolioRepo.CreateAsync(portfolioModel);
            await _portfolioRepo.SaveChangesAsync();

            return portfolio;


        }

        public async Task<Portfolio?> UpdateAsync(UpdatePortfolioDto updateDto)
        {
            var userId = _currentUserService.UserId!.Value;

            var portfolio = await _portfolioRepo.GetByUserIdAsync(userId);

            if (portfolio == null) 
            {
                return null;
            }

            portfolio.ApplyUpdate(updateDto);
            _portfolioRepo.Update(portfolio);
            await _portfolioRepo.SaveChangesAsync();

            return portfolio;
        }

        public async Task<bool> DeleteAsync()
        {
            //var userId = _currentUserService.UserId;
            var portfolio = await GetMyPortfolioAsync();

            if (portfolio == null) 
            {
                return false;
            }

            _portfolioRepo.Delete(portfolio);
            await _portfolioRepo.SaveChangesAsync();
            return true;

        }
    }
}
