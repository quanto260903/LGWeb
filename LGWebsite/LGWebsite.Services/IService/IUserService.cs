using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace AODWebsite.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync(string sortOrder, string currentFilter, string searchString, int? page);
        Task<User> GetUserByIdAsync(int id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(int id, User updatedUser);
        Task DeleteUserAsync(int id);
        Task<(string imageUrl, string thumbnailUrl)> UploadImage(IFormFile imageFile);
        Task<bool> UpdateUserStatusAsync(int id, bool isEnabled);
    }

}
