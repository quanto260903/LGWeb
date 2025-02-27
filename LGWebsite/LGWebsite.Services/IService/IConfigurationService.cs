using Domain.Entities;
using X.PagedList;
namespace AODWebsite.Services
{
    public interface IConfigurationService
    {
        Task<IPagedList<Configuration>> GetAllConfigurationsAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task AddConfigurationAsync(Configuration model);
        Task<Configuration> GetConfigurationByIdAsync(int id);
        Task UpdateConfigurationAsync(Configuration model, string updatedBy);
        Task DeleteConfigurationAsync(int id);

        Task<bool> IsConfigKeyExistsAsync(string configKey, int? excludeId = null);


    }

}
