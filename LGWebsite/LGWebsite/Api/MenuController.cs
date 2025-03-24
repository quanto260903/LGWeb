using LGWebsite.Services.IService;
using Microsoft.AspNetCore.Mvc;

namespace LGWebsite.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            var listMenu = await _menuService.GetAllMenuAsync();
            foreach (var item in listMenu)
            {
                item.ImageUrl = AppSettings.Endpoint + item.ImageUrl;
            }
            return Ok(listMenu);
        }
    }
}
