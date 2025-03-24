using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGWebsite.Services.IService
{
    public interface IAccountService
    {
        Task<bool> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
        Task<string> ForgotPasswordAsync(User model);
        Task<string> ResetPasswordAsync(string email, string token, string newPassword);
    }
}
