using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories
{
    public class ConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
    {
        private readonly AodwebsiteContext _context;
        public ConfigurationRepository(AodwebsiteContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Configuration>> GetConfigurationsAsync(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var query = _context.Configurations.AsQueryable();

            // Lọc theo searchString
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c => c.ConfigKey.Contains(searchString) || c.ConfigValue.Contains(searchString));
            }

            // Sắp xếp theo sortOrder
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(c => c.ConfigKey);
                    break;
                case "Date":
                    query = query.OrderBy(c => c.DateCreated); 
                    break;
                case "date_desc":
                    query = query.OrderByDescending(c => c.DateCreated);
                    break;
                default:
                    query = query.OrderBy(c => c.ConfigKey);
                    break;
            }

            // Phân trang
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<Configuration> FirstOrDefaultAsync(string configKey)
        {
            return await _context.Configurations
                                 .FirstOrDefaultAsync(c => c.ConfigKey == configKey);
        }
    }
}
