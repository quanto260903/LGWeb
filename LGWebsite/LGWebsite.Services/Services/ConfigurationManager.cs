using DataAccess.EFCore;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGWebsite.Services.Services
{
    public sealed class ConfigurationManager
    {
        // Thread-safe dictionary to hold the configurations
        private static readonly ConcurrentDictionary<ConfigurationKeys, WebConfiguration> _configurations = new ConcurrentDictionary<ConfigurationKeys, WebConfiguration>();

        // Lock object for thread-safety
        private static readonly object _lock = new object();

        // Private constructor to prevent instantiation
        private ConfigurationManager() { }

        // Method to get configuration by Enum Key
        public static WebConfiguration GetConfiguration(ConfigurationKeys key)
        {
            if (_configurations.ContainsKey(key))
            {
                return _configurations[key];
            }

            lock (_lock)
            {
                if (!_configurations.ContainsKey(key))
                {
                    using (var context = new LgwebsiteContext())
                    {
                        string keyString = key.ToString();
                        var config = context.WebConfigurations
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
                using (var context = new LgwebsiteContext())
                {
                    string keyString = key.ToString();
                    var config = context.WebConfigurations.FirstOrDefault(c => c.ConfigKey == keyString);

                    if (config != null)
                    {
                        config.ConfigValue = valueAsString;
                        context.WebConfigurations.Update(config);
                    }
                    else
                    {
                        config = new WebConfiguration
                        {
                            ConfigKey = keyString,
                            ConfigValue = valueAsString
                        };
                        context.WebConfigurations.Add(config);
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
}
