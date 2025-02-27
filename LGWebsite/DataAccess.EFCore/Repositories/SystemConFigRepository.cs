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
    public class SystemConFigRepository : GenericRepository<SystemConFig>, ISystemConfigRepository
    {
        public SystemConFigRepository(AodwebsiteContext context) : base(context)
        {
        }
        public async Task<IEnumerable<SystemConFig>> GetConfigurationsAsync(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var query = _context.SystemComFixes.AsQueryable();

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
        public async Task<SystemConFig> FirstOrDefaultAsync(string configKey)
        {
            return await _context.SystemComFixes
                                 .FirstOrDefaultAsync(c => c.ConfigKey == configKey);
        }
    }
}