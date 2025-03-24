using LGWebsite.Services.Common;
using LGWebsite.Services.IService;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LGWebsite.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AuthenticationService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly EmailUtil _emailUtil;

        public AccountService(IUnitOfWork unitOfWork, ILogger<AuthenticationService> logger, IHttpContextAccessor httpContextAccessor, EmailUtil emailUtil)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _emailUtil = emailUtil;
        }


        public async Task<bool> LoginAsync(LoginViewModel model)
        {
            if (model == null)
            {
                return false;
            }

            try
            {
                // Fetch user from database
                var user = await _unitOfWork.User.GetUserByUserNameAsync(model.UserName);

                // Validate the password
                if (user == null || !BCrypt.Net.BCrypt.Verify(model.PassWord, user.PassWord))
                {
                    return false; // Invalid login
                }

                // If user exists, proceed with creating claims
                var imageUrl = user.ImageUrl ?? "~/Url/images/default-avatar.png";

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("imageUrl", imageUrl),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign the user in
                await _httpContextAccessor.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return true; // Successful login
            }
            catch (Exception ex)
            {
                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
                _logger.LogError(ex, "Error occurred during login in {ActionName} at {Time}.", nameof(LoginAsync), vietnamTime);
                return false;
            }
        }

        public async Task LogoutAsync()
        {
            try
            {
                await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            }
            catch (Exception ex)
            {
                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
                _logger.LogError(ex, "Error occurred during logout in {ActionName} at {Time}.", nameof(LogoutAsync), vietnamTime);
            }
        }
        public async Task<string> ForgotPasswordAsync(User model)
        {
            if (string.IsNullOrEmpty(model.Email))
            {
                return "Email không được để trống.";
            }

            try
            {
                var user = await _unitOfWork.User.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    return "Liên kết đặt lại mật khẩu đã được gửi đến email của bạn nếu email tồn tại trong hệ thống.";
                }

                string resetLink = $"https://your-site.com/Account/ResetPassword?email={model.Email}&token=dummy-token";

                bool isEmailSent = await _emailUtil.SendEmail(
                    model.Email,
                    "Đặt lại mật khẩu",
                    $@"
<html>
<body>
    <p>Nhấn vào <a href='{resetLink}'>đây</a> để đặt lại mật khẩu của bạn.</p>
</body>
</html>"
                );

                if (isEmailSent)
                {
                    return "Liên kết đặt lại mật khẩu đã được gửi đến email của bạn nếu email tồn tại trong hệ thống.";
                }
                else
                {
                    return "Đã xảy ra lỗi khi gửi email. Vui lòng thử lại sau.";
                }
            }
            catch (Exception ex)
            {
                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
                _logger.LogError(ex, "An error occurred while sending the password reset email for user at {Time}.", vietnamTime);
                return "Đã xảy ra lỗi khi gửi email. Vui lòng thử lại sau.";
            }
        }

        public async Task<string> ResetPasswordAsync(string email, string token, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token) || string.IsNullOrEmpty(newPassword))
            {
                return "Thông tin không hợp lệ.";
            }

            try
            {
                var user = await _unitOfWork.User.GetUserByEmailAsync(email);
                if (user == null)
                {
                    return "Người dùng không tồn tại.";
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);
                _unitOfWork.User.UpdatePassword(user, hashedPassword);
                await _unitOfWork.User.UpdateAsync(user);
                _unitOfWork.Complete();

                return "Mật khẩu của bạn đã được đặt lại thành công.";
            }
            catch (Exception ex)
            {
                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
                _logger.LogError(ex, "An error occurred while resetting password for user at {Time}.", vietnamTime);
                return "Đã xảy ra lỗi khi đặt lại mật khẩu. Vui lòng thử lại.";
            }
        }
    }
}
