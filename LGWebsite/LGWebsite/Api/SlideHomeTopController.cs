using LGWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LGWebsite.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlideHomeTopController : ControllerBase
    {
        private readonly IBlogsCategoryService _BlogsCategoryService;
        public SlideHomeTopController(IBlogsCategoryService BlogsCategoryService)
        {
            _BlogsCategoryService = BlogsCategoryService;
        }

        [HttpGet("SlideHomeTop")]
        public async Task<IActionResult> GetSlideHome()
        {
            var listslide = await _BlogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_Home_Slide);
            foreach (var item in listslide)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listslide);
        }
    }
}
