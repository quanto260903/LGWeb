using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace LGWebsite.Controllers
{
    public class BaseController : Controller
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var user = context.HttpContext.User;

            // Lấy tên người dùng từ claims
            var username = user.Identity?.Name;
            var imageUrlClaim = user.Claims.FirstOrDefault(c => c.Type == "imageUrl");
            var imageUrl = imageUrlClaim?.Value ?? Url.Content("~/Url/images/default-avatar.png");

            // Thiết lập ViewBag cho mọi trang
            ViewBag.Username = username ?? "Khách";
            ViewBag.ImageUrl = imageUrl;
        }
    }
}
