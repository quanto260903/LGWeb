using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IConfigurationRepository : IGenericRepository<WebConfiguration>
    {
        Task<IEnumerable<WebConfiguration>> GetConfigurationsAsync(string sortOrder, string searchString, int pageNumber, int pageSize);

        Task<WebConfiguration> FirstOrDefaultAsync(string configKey);
    }
}
