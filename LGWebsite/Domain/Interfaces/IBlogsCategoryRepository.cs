using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBlogsCategoryRepository : IGenericRepository<BlogsCategory>
    {
        Task<IEnumerable<BlogsCategory>> GetBlogCategoryAsync(string sortOrder, string searchString, int pageNumber, int pageSize);
    }
}
