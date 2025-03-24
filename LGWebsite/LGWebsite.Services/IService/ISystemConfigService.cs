using Domain.Entities;
using X.PagedList;

namespace LGWebsite.Services
{
    public interface ISystemConfigService
    {
        Task<IPagedList<SystemConfiguration>> GetAllConfigurationsAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<IEnumerable<SystemConfiguration>> GetAllConfigsAsync();
        Task<SystemConfiguration> GetConfigByIdAsync(int id);
        Task AddConfigAsync(SystemConfiguration config);
        Task UpdateConfigAsync(SystemConfiguration config, string updatedBy);
        Task DeleteConfigAsync(int id);
        Task<bool> IsConfigKeyExistsAsync(string configKey, int? excludeId = null);
        Task<string> GetThemeModeAsync();
        }


}
