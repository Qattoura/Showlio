using Showlio.api.Models;

namespace Showlio.api.Interfaces.IRepositories
{
    public interface ITemplateRepository : IRepository<Template>
    {
        Task<IEnumerable<Template>> GetActiveTemplatesAsync();
        Task<Template?> GetActiveTemplateByIdAsync(int id);
        Task<bool> ExistsByTitleAsync(string title, int? excludeId = null);
        Task<bool> IsTemplateUsedAsync(int templateId);
    }
}
