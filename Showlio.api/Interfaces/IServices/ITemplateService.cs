using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Interfaces.IServices
{
    public interface ITemplateService
    {
        public Task<TemplateServiceResult<IEnumerable<Template>>> GetTemplatesAsync();
        Task<TemplateServiceResult<Template>> GetTemplateByIdAsync(int id);
        public Task<TemplateServiceResult<Template>> CreateTemplateAsync(CreateTemplateDto createDto);
        public Task<TemplateServiceResult<Template>> UpdateTemplateAsync(UpdateTemplateDto updateDto, int id);
        public Task<TemplateServiceStatus> DeleteTemplateAsync(int id);
    }
}
