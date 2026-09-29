using Showlio.api.Dtos;
using Showlio.api.Models;

namespace Showlio.api.Mappers
{
    public static class TemplateMapper
    {
        public static TemplateDto ToDto(this Template template)
        {
            return new TemplateDto
            {
                Id = template.Id,
                Title = template.Title,
                Genre = template.Genre,
                Description = template.Description,
                ColorTheme = template.ColorTheme
            };
        }

        public static AdminTemplateDto ToAdminDto(
            this Template template)
        {
            return new AdminTemplateDto
            {
                Id = template.Id,
                Title = template.Title,
                Genre = template.Genre,
                Description = template.Description,
                ColorTheme = template.ColorTheme,
                IsActive = template.IsActive
            };
        }

        public static Template ToEntity(
            this CreateTemplateDto dto)
        {
            return new Template
            {
                Title = dto.Title,
                Genre = dto.Genre,
                Description = dto.Description,
                ColorTheme = dto.ColorTheme
            };
        }

        public static void ApplyUpdate(
            this Template template,
            UpdateTemplateDto dto)
        {
            template.Title = dto.Title;
            template.Genre = dto.Genre;
            template.Description = dto.Description;
            template.ColorTheme = dto.ColorTheme;
            template.IsActive = dto.IsActive;
        }
    }
}
