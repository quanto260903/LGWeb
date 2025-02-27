using Domain.Entities;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using X.PagedList;

namespace AODWebsite.Services
{
    public interface IBlogsCategoryService
    {
        Task<IEnumerable<BlogsCategory>> GetAllAsync();
        Task<IEnumerable<BlogsCategory>> GetBlogCategoryByPositionAsync(string position);
        Task<BlogsCategory> GetByIdAsync(int id);
        Task AddAsync(BlogCategoryModel category);
        Task UpdateAsync(BlogsCategory category);
        Task DeleteAsync(int id);
        (string imageUrl, string thumbnailUrl) UploadImage(IFormFile imageFile);
        Task UpdateBlogsCategoryStatus(int id, bool isDeleted, bool isDarft, bool isHome);
        Task<IPagedList<BlogsCategory>> GetAllBlogsCategoryAsync(string sortOrder, string currentFilter, string searchString, int? page);
    }


}
