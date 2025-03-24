using LGWebsite;
using LGWebsite.Services;
using Microsoft.AspNetCore.Mvc;

namespace LGDWebsite.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IBlogsCategoryService _blogsCategoryService;
        private readonly ICategoryService _categoryService;

        public ServicesController (IBlogsCategoryService blogsCategoryService, ICategoryService categoryService)
        {
            _blogsCategoryService = blogsCategoryService;
            _categoryService = categoryService;
        }

        [HttpGet("ourexpertise")]
        public async Task<IActionResult> GetOurExpertise()
        {
            var ourexpertise = await _blogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_OurExpertise);
            foreach (var item in ourexpertise)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(ourexpertise);
        }

        [HttpGet("technologies")]
        public async Task<IActionResult> GetTechnologies()
        {
            var technologies = await _blogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_Technologies);
            foreach (var item in technologies)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(technologies);
        }
    }
}
