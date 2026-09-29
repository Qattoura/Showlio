using Showlio.api.Dtos;
using Showlio.api.Enums;
using Showlio.api.Interfaces.IRepositories;
using Showlio.api.Interfaces.IServices;
using Showlio.api.Mappers;
using Showlio.api.Models;
using Showlio.api.Results;

namespace Showlio.api.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly ITemplateRepository _templateRepository;
        private readonly ICurrentUserService _currentUserService;

        public TemplateService(
            ITemplateRepository templateRepository,
            ICurrentUserService currentUserService)
        {
            _templateRepository = templateRepository;
            _currentUserService = currentUserService;
        }

        public async Task<TemplateServiceResult<IEnumerable<Template>>> GetTemplatesAsync()
        {
            IEnumerable<Template> templates;

            if (_currentUserService.Role == "Admin")
            {
                templates = await _templateRepository.GetAllAsync();
            }
            else
            {
                templates = await _templateRepository.GetActiveTemplatesAsync();
            }

            return new TemplateServiceResult<IEnumerable<Template>>(
                TemplateServiceStatus.Success,
                templates);
        }

        public async Task<TemplateServiceResult<Template>> GetTemplateByIdAsync(int id)
        {
            Template? template;

            if (_currentUserService.Role == "Admin")
            {
                template = await _templateRepository.GetByIdAsync(id);
            }
            else
            {
                template = await _templateRepository.GetActiveTemplateByIdAsync(id);
            }

            if (template == null)
            {
                return new TemplateServiceResult<Template>(
                    TemplateServiceStatus.TemplateNotFound);
            }

            return new TemplateServiceResult<Template>(
                TemplateServiceStatus.Success,
                template);
        }

        public async Task<TemplateServiceResult<Template>> CreateTemplateAsync(CreateTemplateDto createDto)
        {

            if (await _templateRepository.ExistsByTitleAsync(createDto.Title))
            {
                return new TemplateServiceResult<Template>(
                    TemplateServiceStatus.DuplicateTitle);
            }

            var template = createDto.ToEntity();

            await _templateRepository.CreateAsync(template);
            await _templateRepository.SaveChangesAsync();

            return new TemplateServiceResult<Template>(
                TemplateServiceStatus.Success,
                template);
        }

        public async Task<TemplateServiceResult<Template>> UpdateTemplateAsync(UpdateTemplateDto updateDto, int id)
        {
            var template = await _templateRepository.GetByIdAsync(id);

            if (template == null)
            {
                return new TemplateServiceResult<Template>(
                    TemplateServiceStatus.TemplateNotFound);
            }

            if (await _templateRepository.ExistsByTitleAsync(
                    updateDto.Title,
                    id))
            {
                return new TemplateServiceResult<Template>(
                    TemplateServiceStatus.DuplicateTitle);
            }

            template.ApplyUpdate(updateDto);

            _templateRepository.Update(template);
            await _templateRepository.SaveChangesAsync();

            return new TemplateServiceResult<Template>(
                TemplateServiceStatus.Success,
                template);
        }

        public async Task<TemplateServiceStatus> DeleteTemplateAsync(int id)
        {
            var template = await _templateRepository.GetByIdAsync(id);

            if (template == null)
            {
                return TemplateServiceStatus.TemplateNotFound;
            }

            if (await _templateRepository.IsTemplateUsedAsync(id))
            {
                return TemplateServiceStatus.TemplateInUse;
            }

            _templateRepository.Delete(template);

            await _templateRepository.SaveChangesAsync();

            return TemplateServiceStatus.Success;

        }
    }
}
