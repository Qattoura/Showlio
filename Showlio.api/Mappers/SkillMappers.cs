using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class SkillMappers
    {
        public static SkillDto ToDto(this Skill skill)
        {
            return new SkillDto
            {
                Id = skill.Id,
                PortfolioId = skill.PortfolioId,
                Name = skill.Name,
                DisplayOrder = skill.DisplayOrder
            };
        }
        public static Skill ToEntity(this CreateSkillDto dto, int portfolioId)
        {
            return new Skill
            {
                PortfolioId = portfolioId,
                Name = dto.Name,
                DisplayOrder = dto.DisplayOrder
            };
        }

        public static void ApplyUpdate(this Skill skill, UpdateSkillDto dto)
        {
            skill.Name = dto.Name;
            skill.DisplayOrder = dto.DisplayOrder;
        }
    }
}
