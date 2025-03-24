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
    public class BlogsCategoryRepository : GenericRepository<BlogsCategory>, IBlogsCategoryRepository
    {
        private readonly LgwebsiteContext _context;
        public BlogsCategoryRepository(LgwebsiteContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<BlogsCategory>> GetBlogCategoryAsync(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var query = _context.BlogsCategories.AsQueryable();

            // Lọc theo searchString
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c => c.Name.Contains(searchString) || c.Description.Contains(searchString));
            }

            // Sắp xếp theo sortOrder
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(c => c.Name);
                    break;
                case "Date":
                    query = query.OrderBy(c => c.DateCreated);
                    break;
                case "date_desc":
                    query = query.OrderByDescending(c => c.DateCreated);
                    break;
                default:
                    query = query.OrderBy(c => c.Name);
                    break;
            }

            // Phân trang
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
    }
}
