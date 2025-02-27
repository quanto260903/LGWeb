using AODWebsite.Services;
using AODWebsite.Services.Services;
using DataAccess.EFCore;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using X.PagedList;

public class ConfigurationService : IConfigurationService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ConfigurationService> _logger;
    // Thread-safe dictionary to hold the configurations
    private static readonly ConcurrentDictionary<ConfigurationKeys, Configuration> _configurations = new ConcurrentDictionary<ConfigurationKeys, Configuration>();

    // Lock object for thread-safety
    private static readonly object _lock = new object();


    public ConfigurationService(IUnitOfWork unitOfWork, ILogger<ConfigurationService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<IPagedList<Configuration>> GetAllConfigurationsAsync(string sortOrder, string currentFilter, string searchString, int? page)
    {
        //get configuration item per page
        //var itemPerPage = await _unitOfWork.Configuration.FirstOrDefaultAsync("ItemsPerPage");
        //int pageSize = int.Parse(itemPerPage.ConfigValue);
        //int pageNumber = (page ?? 1);
        var itemPerPage = ConfigurationManager.GetConfigurationValue<int>(ConfigurationKeys.ItemsPerPage);
        int pageSize = itemPerPage;
        int pageNumber = (page ?? 1);


        try
        {
            var configurations = await _unitOfWork.Configuration.GetConfigurationsAsync(sortOrder, searchString, pageNumber, pageSize);
            var totalConfigurations = await _unitOfWork.Configuration.GetConfigurationsAsync(sortOrder, searchString, 1, int.MaxValue);

            return new StaticPagedList<Configuration>(configurations, pageNumber, pageSize, totalConfigurations.Count());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving configurations.");
            return new StaticPagedList<Configuration>(new List<Configuration>(), pageNumber, pageSize, 0);
        }
    }

    public async Task AddConfigurationAsync(Configuration model)
    {
        await _unitOfWork.Configuration.AddAsync(model);
        _unitOfWork.Complete();
    }

    public async Task<Configuration> GetConfigurationByIdAsync(int id)
    {
        return await _unitOfWork.Configuration.GetByIdAsync(id);
    }

    public async Task<bool> IsConfigKeyExistsAsync(string configKey, int? excludeId = null)
    {
        if (excludeId.HasValue)
        {
            return await _unitOfWork.Configuration.FirstOrDefaultAsync(c => c.ConfigKey == configKey && c.Id != excludeId.Value) != null;
        }
        return await _unitOfWork.Configuration.FirstOrDefaultAsync(c => c.ConfigKey == configKey) != null;
    }

    public async Task UpdateConfigurationAsync(Configuration model, string updatedBy)
    {
        var existingConfiguration = await _unitOfWork.Configuration.GetByIdAsync(model.Id);
        if (existingConfiguration != null)
        {
            //compare before
            bool isChangeValue = false;
            if (model.ConfigValue != existingConfiguration.ConfigValue)
                isChangeValue = true;

            existingConfiguration.ConfigValue = model.ConfigValue;
            existingConfiguration.ConfigKey = model.ConfigKey;
            existingConfiguration.Description = model.Description;
            existingConfiguration.DateModified = DateTimeOffset.UtcNow;
            existingConfiguration.UpdateBy = updatedBy;

            _unitOfWork.Complete();

            Enum.TryParse("model.ConfigKey", out ConfigurationKeys keyEnum);

            switch (keyEnum, isChangeValue)
            {
                case (ConfigurationKeys.ItemsPerPage, true):
                    ConfigurationManager.SetConfiguration(ConfigurationKeys.ItemsPerPage, model.ConfigValue);

                    break;
                case (ConfigurationKeys.TimeoutDuration, true):

                    break;
                case (ConfigurationKeys.DefaultLanguage, true):

                    break;
                default:
                    break;
            }
        }
    }

    public async Task DeleteConfigurationAsync(int id)
    {
        var configuration = await _unitOfWork.Configuration.GetByIdAsync(id);
        if (configuration != null)
        {
            await _unitOfWork.Configuration.RemoveAsync(configuration);
             _unitOfWork.Complete();
        }
    }

    // Method to get configuration by Enum Key
    public static Configuration GetConfiguration(ConfigurationKeys key)
    {
        if (_configurations.ContainsKey(key))
        {
            return _configurations[key];
        }

        lock (_lock)
        {
            if (!_configurations.ContainsKey(key))
            {
                using (var context = new AodwebsiteContext())
                {
                    string keyString = key.ToString();
                    var config = context.Configurations
                                        .AsNoTracking()
                                        .FirstOrDefault(c => c.ConfigKey == keyString);

                    if (config != null)
                    {
                        _configurations.TryAdd(key, config);
                    }

                    return config;
                }
            }
        }

        return _configurations[key];
    }

    // Method to set configuration value by Enum Key
    public static void SetConfiguration(ConfigurationKeys key, object value)
    {
        string valueAsString = Convert.ToString(value); // Convert the value to string before storing

        lock (_lock)
        {
            using (var context = new AodwebsiteContext())
            {
                string keyString = key.ToString();
                var config = context.Configurations.FirstOrDefault(c => c.ConfigKey == keyString);

                if (config != null)
                {
                    config.ConfigValue = valueAsString;
                    context.Configurations.Update(config);
                }
                else
                {
                    config = new Configuration
                    {
                        ConfigKey = keyString,
                        ConfigValue = valueAsString
                    };
                    context.Configurations.Add(config);
                }

                context.SaveChanges();
                _configurations.AddOrUpdate(key, config, (k, existingConfig) => config);
            }
        }
    }

    // Generic method to retrieve the value as the desired type
    public static T GetConfigurationValue<T>(ConfigurationKeys key)
    {
        var config = GetConfiguration(key);
        if (config == null || config.ConfigValue == null)
        {
            return default(T);
        }

        // Convert the string value back to the requested type
        return (T)Convert.ChangeType(config.ConfigValue, typeof(T));
    }
}
