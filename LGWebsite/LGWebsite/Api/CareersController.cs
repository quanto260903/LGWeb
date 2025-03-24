using LGWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LGWebsite.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CareersController : ControllerBase
    {
        private readonly IBlogsCategoryService _BlogsCategoryService;
        public CareersController(IBlogsCategoryService BlogsCategoryService)
        {
            _BlogsCategoryService = BlogsCategoryService;
        }
        [HttpGet("careersTop")]
        public async Task<IActionResult> GetCareersTop()
        {
            var listCareers = await _BlogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_Careers_Top);
            foreach (var item in listCareers)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listCareers);
        }

        [HttpGet("careersJob/{id}")]
        public async Task<IActionResult> GetCareersJob(int id)
        {
            // Gọi dịch vụ để lấy dữ liệu công việc theo ID
            var career = await _BlogsCategoryService.GetByIdAsync(id);

            // Kiểm tra xem có dữ liệu không
            if (career == null)
            {
                return NotFound("Job not found 52");
            }

            // Thêm endpoint vào đường dẫn hình ảnh
            career.ImageUrl = AppSettings.Endpoint + career.ImageUrl;

            return Ok(career);
        }


    }
}
