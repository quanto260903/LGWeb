using LGWebsite.Services;
using LGWebsite.Services.IService;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LGWebsite.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IBlogsCategoryService _blogCategoryService;
        private readonly IVideoService _videoService;
        private readonly ICategoryService _categoryService;
        public ProductController(IBlogsCategoryService blogCategoryService, IVideoService videoService, ICategoryService categoryService)
        {
            _blogCategoryService = blogCategoryService;
            _videoService = videoService;
            _categoryService = categoryService;
        }

        [HttpGet("resourcescheduling")]
        public async Task<IActionResult> GetResourcescheduling()
        {
            var resourcescheduling = await _blogCategoryService.GetBlogCategoryByPositionAsync(Position.Position_ResourceScheduling);

            // Kiểm tra xem resourcescheduling có null hoặc rỗng không
            if (resourcescheduling == null || !resourcescheduling.Any())
            {
                return NotFound("No resources found for the specified position.");
            }

            var blogCategoryModels = new List<BlogCategoryModel>();

            foreach (var item in resourcescheduling)
            {
                // Kiểm tra item có null không
                if (item == null || item.CategoryId <= 0)
                {
                    continue; // Bỏ qua nếu item không hợp lệ
                }

                // Khởi tạo đối tượng BlogCategoryModel mới và gán trực tiếp từ item
                var blogCategoryModel = new BlogCategoryModel
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Description = item.Description,
                    IsDarft = item.IsDraft,
                    IsHome = item.IsHome,
                    Order = item.Order,
                    Color = item.Color,
                    ImageUrl = AppSettings.Endpoint + item.ImageUrl,
                    Position = item.Position,
                    Features = item.Features,
                    UpdateBy = item.UpdateBy,
                    DateCreated = item.DateCreated,
                    DateModified = item.DateModified,
                    // Lấy danh sách video cho từng danh mục
                    Videos = await _videoService.GetVideosByBlogCategoryIdAsync(item.Id)

                };

                // Thêm vào danh sách
                blogCategoryModels.Add(blogCategoryModel);
            }

            // Lấy category chính
            var category = await _blogCategoryService.GetByIdAsync(resourcescheduling.FirstOrDefault()?.CategoryId ?? 0);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            var dto = new BlogCategoryModel
            {
                ImageUrl = AppSettings.Endpoint + category.ImageUrl, // Gán trực tiếp, có thể là null
                ListBlogCategory = blogCategoryModels
            };

            return Ok(dto);

        }
        [HttpGet("digitalsignage")]
        public async Task<IActionResult> GetDigitalSignage()
        {
            var resourcescheduling = await _blogCategoryService.GetBlogCategoryByPositionAsync(Position.Position_DigitalSignage);

            // Kiểm tra xem resourcescheduling có null hoặc rỗng không
            if (resourcescheduling == null || !resourcescheduling.Any())
            {
                return NotFound("No resources found for the specified position.");
            }

            var blogCategoryModels = new List<BlogCategoryModel>();

            foreach (var item in resourcescheduling)
            {
                // Kiểm tra item có null không
                if (item == null || item.CategoryId <= 0)
                {
                    continue; // Bỏ qua nếu item không hợp lệ
                }

                // Khởi tạo đối tượng BlogCategoryModel mới và gán trực tiếp từ item
                var blogCategoryModel = new BlogCategoryModel
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Description = item.Description,
                    IsDarft = item.IsDraft,
                    IsHome = item.IsHome,
                    Order = item.Order,
                    Color = item.Color,
                    ImageUrl = AppSettings.Endpoint + item.ImageUrl,
                    Position = item.Position,
                    Features = item.Features,
                    UpdateBy = item.UpdateBy,
                    DateCreated = item.DateCreated,
                    DateModified = item.DateModified,
                    // Lấy danh sách video cho từng danh mục
                    Videos = await _videoService.GetVideosByBlogCategoryIdAsync(item.Id)

                };

                // Thêm vào danh sách
                blogCategoryModels.Add(blogCategoryModel);
            }

            // Lấy category chính
            var category = await _blogCategoryService.GetByIdAsync(resourcescheduling.FirstOrDefault()?.CategoryId ?? 0);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            var dto = new BlogCategoryModel
            {
                ImageUrl = AppSettings.Endpoint + category.ImageUrl, // Gán trực tiếp, có thể là null
                ListBlogCategory = blogCategoryModels
            };

            return Ok(dto);
        }
        [HttpGet("exchangeandoutlook")]
        public async Task<IActionResult> GetExchangeandOutlook()
        {
            var resourcescheduling = await _blogCategoryService.GetBlogCategoryByPositionAsync(Position.Position_ExchangeOutlook);

            // Kiểm tra xem resourcescheduling có null hoặc rỗng không
            if (resourcescheduling == null || !resourcescheduling.Any())
            {
                return NotFound("No resources found for the specified position.");
            }

            var blogCategoryModels = new List<BlogCategoryModel>();

            foreach (var item in resourcescheduling)
            {
                // Kiểm tra item có null không
                if (item == null || item.CategoryId <= 0)
                {
                    continue; // Bỏ qua nếu item không hợp lệ
                }

                // Khởi tạo đối tượng BlogCategoryModel mới và gán trực tiếp từ item
                var blogCategoryModel = new BlogCategoryModel
                {
                    Id = item.Id,
                    CategoryId = item.CategoryId,
                    Name = item.Name,
                    Description = item.Description,
                    IsDarft = item.IsDraft,
                    IsHome = item.IsHome,
                    Order = item.Order,
                    Color = item.Color,
                    ImageUrl = AppSettings.Endpoint + item.ImageUrl,
                    Position = item.Position,
                    Features = item.Features,
                    UpdateBy = item.UpdateBy,
                    DateCreated = item.DateCreated,
                    DateModified = item.DateModified,
                    // Lấy danh sách video cho từng danh mục
                    Videos = await _videoService.GetVideosByBlogCategoryIdAsync(item.Id)

                };

                // Thêm vào danh sách
                blogCategoryModels.Add(blogCategoryModel);
            }

            // Lấy category chính
            var category = await _blogCategoryService.GetByIdAsync(resourcescheduling.FirstOrDefault()?.CategoryId ?? 0);
            if (category == null)
            {
                return NotFound("Category not found.");
            }

            var dto = new BlogCategoryModel
            {
                ImageUrl = AppSettings.Endpoint + category.ImageUrl, // Gán trực tiếp, có thể là null
                ListBlogCategory = blogCategoryModels
            };

            return Ok(dto);
        }

        [HttpGet("positiontab")]
        public async Task<IActionResult> GetAllProduct()
        {
            var product = await _categoryService.GetCategoryByPositionAsync(PositionTab.Position_Product);
            foreach (var item in product)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(product);
        }
    }
}
