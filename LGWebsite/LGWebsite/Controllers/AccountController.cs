using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Domain.Interfaces;
using DataAccess.EFCore;
using DataAccess.EFCore.UnitOfWorks;
using Domain.Models;
using LGWebsite.Services.IService;

namespace LGWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;
        public AccountController( ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _accountService.LoginAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác.");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(User model)
        {
            var message = await _accountService.ForgotPasswordAsync(model);
            ViewBag.Message = message;
            return View(model);
        }


        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            // Trang này sẽ được hiển thị khi người dùng nhấp vào liên kết trong email
            ViewBag.Email = email;
            ViewBag.Token = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string email, string token, string newPassword)
        {
            var message = await _accountService.ResetPasswordAsync(email, token, newPassword);
            ViewBag.Message = message;
            return View();
        }
        public IActionResult SomeAction()
        {
            // Lấy tên người dùng từ claims
            var username = User.Identity.Name; // Trả về tên người dùng từ claims
            var imageUrlClaim = User.Claims.FirstOrDefault(c => c.Type == "imageUrl");
            var imageUrl = imageUrlClaim != null ? imageUrlClaim.Value : Url.Content("~/Url/images/default-avatar.png");
            ViewBag.ImageUrl = imageUrl;

            // Gửi tên người dùng tới view
            ViewBag.Username = username ?? "Khách"; // Nếu không có, hiển thị "Khách"
            ViewBag.ImageUrl = imageUrl;
            return View();
        }
    }
}
