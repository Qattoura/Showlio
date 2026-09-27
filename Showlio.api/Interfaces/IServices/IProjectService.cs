using Showlio.api.Dtos;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Interfaces.IServices
{
    public interface IProjectService
    {
        Task<ProjectServiceResult<IEnumerable<Project>>> GetAllProjectsForPortfolioAsync(int portfolioId);
        public Task<ProjectServiceResult<Project>> GetProjectAsync(int projectId, int portfolioId);
        Task<ProjectServiceResult<Project>> CreateAsync(CreateProjectDto createDto, int portfolioId);
        Task<ProjectServiceResult<Project>> UpdateAsync(UpdateProjectDto updateDto, int projectId, int portfolioId);
        Task<ProjectServiceResult<bool>> DeleteAsync(int projectId, int portfolioId);
    }
}
