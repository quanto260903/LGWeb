using LGWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LGWebsite.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        private readonly IBlogsCategoryService _BlogsCategoryService;
        public AboutController(IBlogsCategoryService BlogsCategoryService)
        {
            _BlogsCategoryService = BlogsCategoryService;
        }
        [HttpGet("aboutus")]
        public async Task<IActionResult> GetAboutTop()
        {
            var listAbout = await _BlogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_AboutUs_Top);
            foreach (var item in listAbout)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listAbout);
        }
        [HttpGet("strategic")]
        public async Task<IActionResult> GetAboutStrategic()
        {
            var listAbout = await _BlogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_AboutUs_Strategic);
            foreach (var item in listAbout)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listAbout);
        }
        [HttpGet("ceo")]
        public async Task<IActionResult> GetAboutCEO()
        {
            var listAbout = await _BlogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_AboutUs_Ceo);
            foreach (var item in listAbout)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listAbout);
        }

        [HttpGet("team")]
        public async Task<IActionResult> GetAboutTeam()
        {
            var listAbout = await _BlogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_AboutUs_Team);
            foreach (var item in listAbout)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listAbout);
        }
    }
}
