
using DataAccess.EFCore.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LGWebsite.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync(string sortOrder, string currentFilter, string searchString, int? page)
        {
            //get configuration item per page
            var itemPerPage = await _unitOfWork.Configuration.FirstOrDefaultAsync("ItemsPerPage");
            int pageSize = int.Parse(itemPerPage.ConfigValue);
            int pageNumber = (page ?? 1);

            try
            {
                var configurations = await _unitOfWork.User.GetUserAsync(sortOrder, searchString, pageNumber, pageSize);
                var totalConfigurations = await _unitOfWork.User.GetUserAsync(sortOrder, searchString, 1, int.MaxValue);

                return new StaticPagedList<User>(configurations, pageNumber, pageSize, totalConfigurations.Count());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving configurations.");
                return new StaticPagedList<User>(new List<User>(), pageNumber, pageSize, 0);
            }
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _unitOfWork.User.GetByIdAsync(id);
        }

        public async Task AddUserAsync(User user)
        {
            var existingUser = await _unitOfWork.User.FirstOrDefaultAsync(u => u.UserName == user.UserName);
            if (existingUser != null)
            {
                throw new Exception("User  already exists");
            }

            if (user.ImageFile != null && user.ImageFile.Length > 0)
            {
                var repository = _unitOfWork.User as GenericRepository<User>;
                var (imageUrl, thumbnailUrl) = repository.UploadImage(user.ImageFile);
                user.ImageUrl = imageUrl;
                user.ThumbnailUrl = thumbnailUrl;
            }
            user.UserName = user.UserName;
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.PassWord);
            user.PassWord = hashedPassword;
            user.DateCreated = DateTimeOffset.Now;
            user.DateModified = DateTimeOffset.Now;
            await _unitOfWork.User.AddAsync(user);
            _unitOfWork.Complete();
        }

        public async Task UpdateUserAsync(int id, User updatedUser)
        {
            var user = await _unitOfWork.User.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            if (updatedUser.ImageFile != null && updatedUser.ImageFile.Length > 0)
            {
                var (imageUrl, thumbnailUrl) = await UploadImage(updatedUser.ImageFile);
                user.ImageUrl = imageUrl;
                user.ThumbnailUrl = thumbnailUrl;
            }
            user.Role = updatedUser.Role;
            user.UserName = updatedUser.UserName;
            user.Email = updatedUser.Email;
            user.DateModified = DateTimeOffset.Now;

            await _unitOfWork.User.UpdateAsync(user);
            _unitOfWork.Complete();
        }
        public async Task DeleteUserAsync(int id)
        {
            var slide = await _unitOfWork.User.GetByIdAsync(id);
            if (slide == null) throw new Exception("User not found");

            await _unitOfWork.User.RemoveAsync(slide);
            _unitOfWork.Complete();
        }

        public async Task<(string imageUrl, string thumbnailUrl)> UploadImage(IFormFile imageFile)
        {
            var repository = _unitOfWork.User as GenericRepository<User>;
            return repository.UploadImage(imageFile);
        }

        public async Task<bool> UpdateUserStatusAsync(int id, bool isEnabled)
        {
            var user = await _unitOfWork.User.GetByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            user.Active = isEnabled;
            await _unitOfWork.User.UpdateAsync(user);
            _unitOfWork.Complete();
            return true;
        }

    }




}
