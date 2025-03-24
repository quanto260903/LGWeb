
using DataAccess.EFCore.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace LGWebsite.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            try
            {
                return await _unitOfWork.Category.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching categories.");
                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            try
            {
                return await _unitOfWork.Category.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching category with ID {id}.");
                throw;
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            try
            {

                if (category.ImageFile != null && category.ImageFile.Length > 0)
                {
                    var repository = _unitOfWork.Category as GenericRepository<Category>;
                    var (imageUrl, thumbnailUrl) = repository.UploadImage(category.ImageFile);
                    category.ImageUrl = imageUrl;
                    category.ThumbnailUrl = thumbnailUrl;
                }

                category.DateCreated = DateTimeOffset.Now;
                category.DateModified = DateTimeOffset.Now;

                await _unitOfWork.Category.AddAsync(category);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding category.");
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category, int id)
        {
            try
            {
                var existingCategory = await _unitOfWork.Category.GetByIdAsync(id);
                if (existingCategory == null) throw new Exception("Category not found");

                if (category.ImageFile != null && category.ImageFile.Length > 0)
                {
                    var repository = _unitOfWork.Category as GenericRepository<Category>;
                    var (imageUrl, thumbnailUrl) = repository.UploadImage(category.ImageFile);
                    existingCategory.ImageUrl = imageUrl;
                    existingCategory.ThumbnailUrl = thumbnailUrl;
                }

                existingCategory.Name = category.Name;
                existingCategory.DateModified = DateTimeOffset.Now;

                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category.");
                throw;
            }
        }

        public async Task DeleteCategoryAsync(int id)
        {
            try
            {
                var category = await _unitOfWork.Category.GetByIdAsync(id);
                if (category != null)
                {
                    await _unitOfWork.Category.RemoveAsync(category);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting category.");
                throw;
            }
        }

        public (string imageUrl, string thumbnailUrl) UploadImage(IFormFile imageFile)
        {
            // Your image upload logic goes here
            // Return a tuple with the image and thumbnail URLs
            return ("imageUrl", "thumbnailUrl");
        }

        public async Task UpdateCategoryStatusAsync(int id, bool isEnabled)
        {
            try
            {
                var category = await _unitOfWork.Category.GetByIdAsync(id);
                if (category != null)
                {
                    category.IsDeleted = isEnabled;
                    await _unitOfWork.Category.UpdateAsync(category);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category status.");
                throw;
            }
        }
        public async Task<IEnumerable<Category>> GetCategoryByPositionAsync(string position)
        {
            return await _unitOfWork.Category.FindAsync(p => p.PositionTab == position);
        }
    }


}
