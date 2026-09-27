using System.ComponentModel.DataAnnotations;

namespace Showlio.api.Dtos
{
    public class SkillDto
    {
        public int Id { get; set; }
        public int PortfolioId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
    }

    public class CreateSkillDto
    {
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }

    public class UpdateSkillDto
    {
        public string Name { get; set; } = string.Empty;

        public int DisplayOrder { get; set; }
    }
}
