using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace LGWebsite.Services
{
    public interface ICategoryService

    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category, int id);
        Task DeleteCategoryAsync(int id);
        (string imageUrl, string thumbnailUrl) UploadImage(IFormFile imageFile);
        Task UpdateCategoryStatusAsync(int id, bool isEnabled);
        Task<IEnumerable<Category>> GetCategoryByPositionAsync(string position);
    }

}
