using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Repositories
{
    public class SystemConFigRepository : GenericRepository<SystemConfiguration>, ISystemConfigRepository
    {
        public SystemConFigRepository(LgwebsiteContext context) : base(context)
        {
        }
        public async Task<IEnumerable<SystemConfiguration>> GetConfigurationsAsync(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var query = _context.SystemConfigurations.AsQueryable();

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
        public async Task<SystemConfiguration> FirstOrDefaultAsync(string configKey)
        {
            return await _context.SystemConfigurations
                                 .FirstOrDefaultAsync(c => c.ConfigKey == configKey);
        }
    }
}