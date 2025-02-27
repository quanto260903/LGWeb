
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISystemConfigRepository : IGenericRepository<SystemConFig>
    {
        Task<IEnumerable<SystemConFig>> GetConfigurationsAsync(string sortOrder, string searchString, int pageNumber, int pageSize);
        Task<SystemConFig> FirstOrDefaultAsync(string configKey);
    }
}
