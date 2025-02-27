using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IConfigurationRepository : IGenericRepository<Configuration>
    {
        Task<IEnumerable<Configuration>> GetConfigurationsAsync(string sortOrder, string searchString, int pageNumber, int pageSize);

        Task<Configuration> FirstOrDefaultAsync(string configKey);
    }
}
