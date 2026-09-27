using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class ProjectMappers
    {
        public static ProjectDto ToDto(this Project project)
        {
            return new ProjectDto
            {
                Id = project.Id,
                PortfolioId = project.PortfolioId,
                Title = project.Title,
                Description = project.Description,
                Image = project.Image,
                ProjectLink = project.ProjectLink,
                DisplayOrder = project.DisplayOrder
            };
        }

        public static Project ToEntity(this CreateProjectDto dto, int portfolioId)
        {
            return new Project
            {
                PortfolioId = portfolioId,
                Title = dto.Title,
                Description = dto.Description,
                Image = dto.Image,
                ProjectLink = dto.ProjectLink,
                DisplayOrder = dto.DisplayOrder
            };
        }

        public static void ApplyUpdate(this Project project, UpdateProjectDto dto)
        {
            project.Title = dto.Title;
            project.Description = dto.Description;
            project.Image = dto.Image;
            project.ProjectLink = dto.ProjectLink;
            project.DisplayOrder = dto.DisplayOrder;
        }

    }
}
