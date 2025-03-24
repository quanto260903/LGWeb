using LGWebsite.Models;
using LGWebsite.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LGWebsite.Api
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MethodResourcesController : ControllerBase
    {
        private readonly IBlogsCategoryService _blogsCategoryService;
        private readonly ICategoryService _categoryService;
        public MethodResourcesController(IBlogsCategoryService blogsCategoryService, ICategoryService categoryService)
        {
            _blogsCategoryService = blogsCategoryService;
            _categoryService = categoryService;
        }
        [HttpGet("agilemodel")]
        public async Task<IActionResult> GetAgileModel()
        {
            var listAgile = await _blogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_AgileModel);
            int categoryId = 0;
            foreach (var item in listAgile)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
                categoryId = item.CategoryId;
            }

            var cate = await _categoryService.GetCategoryByIdAsync(categoryId);

            var dto = new AgileModelDto()
            {
                imageUrl = AppSettings.Endpoint + cate?.ImageUrl,
                listBlogCategory = listAgile.ToList()
            };

            return Ok(dto);
        }
        [HttpGet("appliedtools")]
        public async Task<IActionResult> GetAppliedTools()
        {
            var appliedtools = await _blogsCategoryService.GetBlogCategoryByPositionAsync(Position.Position_Appliedtools);
            foreach(var item in appliedtools)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(appliedtools);
        }
    }
}
