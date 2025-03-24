
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using X.PagedList;

namespace LGWebsite.Services
{
    public class SystemConfigService : ISystemConfigService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SystemConfigService> _logger;

        public SystemConfigService(IUnitOfWork unitOfWork, ILogger<SystemConfigService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IPagedList<SystemConfiguration>> GetAllConfigurationsAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //get configuration item per page
            var itemPerPage = await _unitOfWork.Configuration.FirstOrDefaultAsync("ItemsPerPage");
            int pageSize = int.Parse(itemPerPage.ConfigValue);
            int pageNumber = (page ?? 1);

            try
            {
                var configurations = await _unitOfWork.SystemConFig.GetConfigurationsAsync(sortOrder, searchString, pageNumber, pageSize);
                var totalConfigurations = await _unitOfWork.SystemConFig.GetConfigurationsAsync(sortOrder, searchString, 1, int.MaxValue);

                return new StaticPagedList<SystemConfiguration>(configurations, pageNumber, pageSize, totalConfigurations.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving configurations.");
                return new StaticPagedList<SystemConfiguration>(new List<SystemConfiguration>(), pageNumber, pageSize, 0);
            }
        }
        public async Task<IEnumerable<SystemConfiguration>> GetAllConfigsAsync()
        {
            return await _unitOfWork.SystemConFig.GetAllAsync();
        }

        public async Task<SystemConfiguration> GetConfigByIdAsync(int id)
        {
            return await _unitOfWork.SystemConFig.GetByIdAsync(id);
        }

        public async Task AddConfigAsync(SystemConfiguration config)
        {

            // Check for unique ConfigKey
            if (await IsConfigKeyExistsAsync(config.ConfigKey))
            {
                throw new InvalidOperationException($"ConfigKey '{config.ConfigKey}' already exists.");
            }

            config.DateCreated = DateTimeOffset.Now;
            config.DateModified = DateTimeOffset.Now;

            await _unitOfWork.SystemConFig.AddAsync(config);
            _unitOfWork.Complete();
        }

        public async Task UpdateConfigAsync(SystemConfiguration config, string updatedBy)
        {
            // Check for unique ConfigKey, excluding the current config
            if (await IsConfigKeyExistsAsync(config.ConfigKey, config.Id))
            {
                throw new InvalidOperationException($"ConfigKey '{config.ConfigKey}' already exists.");
            }
            config.UpdateBy = updatedBy;
            config.DateModified = DateTimeOffset.Now;
            await _unitOfWork.SystemConFig.UpdateAsync(config);
            _unitOfWork.Complete();
        }

        public async Task DeleteConfigAsync(int id)
        {
            var config = await _unitOfWork.SystemConFig.GetByIdAsync(id);
            if (config != null)
            {
                await _unitOfWork.SystemConFig.RemoveAsync(config);
                _unitOfWork.Complete();
            }
        }

        public async Task<bool> IsConfigKeyExistsAsync(string configKey, int? excludeId = null)
        {
            if (excludeId.HasValue)
            {
                return await _unitOfWork.SystemConFig.FirstOrDefaultAsync(c => c.ConfigKey == configKey && c.Id != excludeId.Value) != null;
            }
            return await _unitOfWork.SystemConFig.FirstOrDefaultAsync(c => c.ConfigKey == configKey) != null;
        }
        public async Task<string> GetThemeModeAsync()
        {
            var modeConfig = await _unitOfWork.SystemConFig.FirstOrDefaultAsync("Mode");
            return modeConfig?.ConfigValue?.ToLower() == "dark" ? "dark" : "light";
        }
    }

}
