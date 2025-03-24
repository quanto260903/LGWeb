using LGWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LGWebsite.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SlideController : ControllerBase
    {
        private readonly IBlogsCategoryService _BlogsCategoryService;
        public SlideController(IBlogsCategoryService BlogsCategoryService)
        {
            _BlogsCategoryService = BlogsCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetSlide()
        {
            var listSlide = await _BlogsCategoryService.GetAllAsync();
            foreach (var item in listSlide)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listSlide);
        }
    }
}
