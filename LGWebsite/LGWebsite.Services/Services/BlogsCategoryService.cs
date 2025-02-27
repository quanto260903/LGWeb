
using AODWebsite.Services.Services;
using DataAccess.EFCore.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AODWebsite.Services
{
    public class BlogsCategoryService : IBlogsCategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BlogsCategoryService> _logger;

        public BlogsCategoryService(IUnitOfWork unitOfWork, ILogger<BlogsCategoryService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<BlogsCategory>> GetAllAsync()
        {
            return await _unitOfWork.BlogsCategory.GetAllAsync();
        }
       
        public async Task<IPagedList<BlogsCategory>> GetAllBlogsCategoryAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            var itemPerPage = ConfigurationManager.GetConfigurationValue<int>(ConfigurationKeys.ItemsPerPage);
            int pageSize = itemPerPage;
            int pageNumber = (page ?? 1);


            try
            {
                var configurations = await _unitOfWork.BlogsCategory.GetBlogCategoryAsync(sortOrder, searchString, pageNumber, pageSize);
                var totalConfigurations = await _unitOfWork.BlogsCategory.GetBlogCategoryAsync(sortOrder, searchString, 1, int.MaxValue);

                return new StaticPagedList<BlogsCategory>(configurations, pageNumber, pageSize, totalConfigurations.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving configurations.");
                return new StaticPagedList<BlogsCategory>(new List<BlogsCategory>(), pageNumber, pageSize, 0);
            }
        }
        public async Task<IEnumerable<BlogsCategory>> GetBlogCategoryByPositionAsync(string position)
        {
            return await _unitOfWork.BlogsCategory.FindAsync(p=> p.Position == position);
        }

        public async Task<BlogsCategory> GetByIdAsync(int id)
        {
            return await _unitOfWork.BlogsCategory.GetByIdAsync(id);
        }

        public async Task AddAsync(BlogCategoryModel category)
        {
            if (category.ImageFile != null && category.ImageFile.Length > 0)
            {
                var repository = _unitOfWork.BlogsCategory as GenericRepository<BlogsCategory>;
                var (imageUrl, thumbnailUrl) = repository.UploadImage(category.ImageFile);
                category.ImageUrl = imageUrl;
                category.ThumbnailUrl = thumbnailUrl;
            }
            var blogsCategory = new BlogsCategory
            {
                Id = category.Id,
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                Detail = category.Detail,
                IsDeleted = category.IsDeleted,
                IsDarft = category.IsDarft,
                ImageUrl = category.ImageUrl,
                ThumbnailUrl = category.ThumbnailUrl,
                DateCreated = DateTimeOffset.Now,
                Position= category.Position,
                Features = category.Features,
                DateModified = category.DateModified
            };
            category.DateCreated = DateTimeOffset.Now;
            await _unitOfWork.BlogsCategory.AddAsync(blogsCategory);
            _unitOfWork.Complete();
            var blogsCategoryId = blogsCategory.Id;

            // Xử lý danh sách video nếu có
            if (category.Videos != null && category.Videos.Any())
            {
                var videoEntities = category.Videos.Select(video => new Video
                {BlogCategoryId = blogsCategory.Id,
                    LinkUrl = video.LinkUrl,
                    Description = video.Description
                }).ToList();

                // Thêm danh sách video vào bảng Video
                await _unitOfWork.Video.AddRangeAsync(videoEntities);
                _unitOfWork.Complete();
            }
        }

        public async Task UpdateAsync(BlogsCategory category)
        {
            if (category.ImageFile != null && category.ImageFile.Length > 0)
            {
                var repository = _unitOfWork.BlogsCategory as GenericRepository<BlogsCategory>;
                var (imageUrl, thumbnailUrl) = repository.UploadImage(category.ImageFile);
                category.ImageUrl = imageUrl;
                category.ThumbnailUrl = thumbnailUrl;
            }
         

            category.DateModified = DateTimeOffset.Now;
            await _unitOfWork.BlogsCategory.UpdateAsync(category);
            _unitOfWork.Complete();
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _unitOfWork.BlogsCategory.GetByIdAsync(id);
            if (category != null)
            {
                await _unitOfWork.BlogsCategory.RemoveAsync(category);
                _unitOfWork.Complete();
            }
        }

        public (string imageUrl, string thumbnailUrl) UploadImage(IFormFile imageFile)
        {
            var repository = _unitOfWork.BlogsCategory as GenericRepository<BlogsCategory>;
            return repository.UploadImage(imageFile);
        }

        public async Task UpdateBlogsCategoryStatus(int id, bool isDeleted, bool isDarft, bool isHome)
        {
            var category = await _unitOfWork.BlogsCategory.GetByIdAsync(id);
            if (category != null)
            {
                category.IsDeleted = isDeleted;
                category.IsDarft = isDarft;
                category.IsHome = isHome;
                await _unitOfWork.BlogsCategory.UpdateAsync(category);
                _unitOfWork.Complete();
            }
        }
    }


}
