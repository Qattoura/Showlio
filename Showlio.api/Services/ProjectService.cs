using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Globals;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepo;
        private readonly IPortfolioAuthorizationService _portfolioAuthoService;
        public ProjectService(IProjectRepository projectRepository, 
            IPortfolioAuthorizationService portfolioAuthorizationService)
        {
            _projectRepo = projectRepository;
            _portfolioAuthoService = portfolioAuthorizationService;
 
            
        }


        // return how many projects per portfolio
        private async Task<int> GetProjectCountAsync(int portfolioId)
        {
            return await _projectRepo.CountProjectsForPortfolioAsync(portfolioId);
        }

        public async Task<ProjectServiceResult<IEnumerable<Project>>> GetAllProjectsForPortfolioAsync(int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ProjectServiceResult<IEnumerable<Project>>(ProjectServiceStatus.PortfolioNotOwned);
            }

            var projects = await _projectRepo.GetAllForPortfolioAsync(portfolioId);

            return new ProjectServiceResult<IEnumerable<Project>>(ProjectServiceStatus.Success, projects);
        }

        public async Task<ProjectServiceResult<Project>> GetProjectAsync(int projectId, int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ProjectServiceResult<Project>(ProjectServiceStatus.PortfolioNotOwned);
            }

            var project = await _projectRepo.GetByIdForPortfolioAsync(projectId, portfolioId);

            if (project == null)
            {
                return new ProjectServiceResult<Project>(ProjectServiceStatus.ProjectNotFound);
            }

            return new ProjectServiceResult<Project>(ProjectServiceStatus.Success, project);
        }

        public async Task<ProjectServiceResult<Project>> CreateAsync(CreateProjectDto createDto, int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ProjectServiceResult<Project>(ProjectServiceStatus.PortfolioNotOwned);
            }

            // Maximum 10 projects
            if (await GetProjectCountAsync(portfolioId) >= GlobalConstants.ProjectMax)
            {
                return new ProjectServiceResult<Project>(ProjectServiceStatus.ProjectLimitReached);
            }

            var project = await _projectRepo.CreateAsync(createDto.ToEntity(portfolioId));

            await _projectRepo.SaveChangesAsync();

            return new ProjectServiceResult<Project>(ProjectServiceStatus.Success, project);


        }
        public async Task<ProjectServiceResult<Project>> UpdateAsync(UpdateProjectDto updateDto, int projectId, int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ProjectServiceResult<Project>(ProjectServiceStatus.PortfolioNotOwned);
            }

            var project = await _projectRepo.GetByIdForPortfolioAsync(projectId, portfolioId);

            if (project == null)
            {
                return new ProjectServiceResult<Project>(ProjectServiceStatus.ProjectNotFound);
            }

            project.ApplyUpdate(updateDto);

            _projectRepo.Update(project);
            await _projectRepo.SaveChangesAsync();

            return new ProjectServiceResult<Project>(ProjectServiceStatus.Success, project);


        }

        public async Task<ProjectServiceResult<bool>> DeleteAsync(int projectId, int portfolioId)
        {
            var isOwner = await _portfolioAuthoService.IsOwnedByCurrentUserAsync(portfolioId);

            if (!isOwner)
            {
                return new ProjectServiceResult<bool>(ProjectServiceStatus.PortfolioNotOwned);
            }

            var project = await _projectRepo.GetByIdForPortfolioAsync(projectId, portfolioId);

            if (project == null)
            {
                return new ProjectServiceResult<bool>(ProjectServiceStatus.ProjectNotFound);
            }

            _projectRepo.Delete(project);
            await _projectRepo.SaveChangesAsync();
            return new ProjectServiceResult<bool>(ProjectServiceStatus.Success, true);

        }

    }
}
