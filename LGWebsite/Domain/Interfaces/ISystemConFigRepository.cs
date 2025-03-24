
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISystemConfigRepository : IGenericRepository<SystemConfiguration>
    {
        Task<IEnumerable<SystemConfiguration>> GetConfigurationsAsync(string sortOrder, string searchString, int pageNumber, int pageSize);
        Task<SystemConfiguration> FirstOrDefaultAsync(string configKey);
    }
}
