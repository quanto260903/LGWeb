using Domain.Entities;
using X.PagedList;
namespace LGWebsite.Services
{
    public interface IConfigurationService
    {
        Task<IPagedList<WebConfiguration>> GetAllConfigurationsAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task AddConfigurationAsync(WebConfiguration model);
        Task<WebConfiguration> GetConfigurationByIdAsync(int id);
        Task UpdateConfigurationAsync(WebConfiguration model, string updatedBy);
        Task DeleteConfigurationAsync(int id);

        Task<bool> IsConfigKeyExistsAsync(string configKey, int? excludeId = null);


    }

}
