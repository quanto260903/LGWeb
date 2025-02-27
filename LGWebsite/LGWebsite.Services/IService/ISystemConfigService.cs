using Domain.Entities;
using X.PagedList;

namespace AODWebsite.Services
{
    public interface ISystemConfigService
    {
        Task<IPagedList<SystemConFig>> GetAllConfigurationsAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<IEnumerable<SystemConFig>> GetAllConfigsAsync();
        Task<SystemConFig> GetConfigByIdAsync(int id);
        Task AddConfigAsync(SystemConFig config);
        Task UpdateConfigAsync(SystemConFig config, string updatedBy);
        Task DeleteConfigAsync(int id);
        Task<bool> IsConfigKeyExistsAsync(string configKey, int? excludeId = null);
        Task<string> GetThemeModeAsync();
        }


}
