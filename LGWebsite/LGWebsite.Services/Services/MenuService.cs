using AODWebsite.Services.IService;
using DataAccess.EFCore.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AODWebsite.Services.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MenuService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MenuService(IUnitOfWork unitOfWork, ILogger<MenuService> logger, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor; // Assign IHttpContextAccessor
        }

        public async Task<IEnumerable<Menu>> GetAllMenuAsync()
        {
            try
            {
                return await _unitOfWork.Menu.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching categories.");
                throw;
            }
        }



        public async Task<MenuCreateViewModel> MenuCreateViewModelAsync()
        {
            var allMenus = await _unitOfWork.Menu.GetAllAsync();
            var menuTree = BuildMenuTree(allMenus);

            return new MenuCreateViewModel
            {
                Menu = new Menu(),
                MenuItems = menuTree ?? new List<MenuCreateViewModel.MenuItem>()
            };
        }

        public async Task<bool> CreateMenuAsync(MenuCreateViewModel data)
        {
            try
            {
                if (data.ParentId.HasValue)
                {
                    data.Menu.ParentId = data.ParentId.Value;
                }

                // Image and Icon handling
                var repository = _unitOfWork.Menu as GenericRepository<Menu>;
                if (data.Menu.ImageFile != null && data.Menu.ImageFile.Length > 0)
                {
                    var (imageUrl, thumbnailUrl) = repository.UploadImage(data.Menu.ImageFile);
                    data.Menu.ImageUrl = imageUrl;
                    data.Menu.ThumbnailUrl = thumbnailUrl;
                }

                if (data.Menu.IconFile != null && data.Menu.IconFile.Length > 0)
                {
                    data.Menu.Icon = repository.UploadIcon(data.Menu.IconFile);
                }

                // Set other fields
                data.Menu.DateCreated = DateTimeOffset.Now;
                data.Menu.DateModified = DateTimeOffset.Now;

                // Add Menu to database
                await _unitOfWork.Menu.AddAsync(data.Menu);
                _unitOfWork.Complete();

                return true;
            }
            catch (Exception ex)
            {
                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

                _logger.LogError(ex, "An error occurred in {ActionName} at {Time}. Stack Trace: {StackTrace}",
                    nameof(CreateMenuAsync), vietnamTime, ex.StackTrace);
                return false;
            }
        }

        private List<MenuCreateViewModel.MenuItem> BuildMenuTree(IEnumerable<Menu> menus, int parentId = 0, int level = 0)
        {
            var result = new List<MenuCreateViewModel.MenuItem>();
            var children = menus.Where(m => m.ParentId == parentId);

            foreach (var child in children)
            {
                var menuItem = new MenuCreateViewModel.MenuItem
                {
                    Id = child.Id,
                    Name = child.Name,
                    Level = level,
                    // Thêm các mục con vào thuộc tính Children
                    Children = BuildMenuTree(menus, child.Id, level + 1)
                };

                result.Add(menuItem);
            }

            return result;
        }
        // Phương thức lấy menu và cây menu để hiển thị form Edit
        public async Task<MenuCreateViewModel> MenuEditAsync(int id)
        {
            var menu = await _unitOfWork.Menu.GetByIdAsync(id);
            if (menu == null)
            {
                return null;
            }

            var allMenus = await _unitOfWork.Menu.GetAllAsync();
            var menuTree = BuildMenuTree(allMenus);

            var menuModel = new MenuCreateViewModel
            {
                Menu = new Menu
                {
                    Id = menu.Id,
                    PositionType = menu.PositionType,
                    Name = menu.Name,
                    ImageUrl = menu.ImageUrl,
                    Icon = menu.Icon,
                    ParentId = menu.ParentId,
                    SortOrder = menu.SortOrder
                },
                MenuItems = menuTree ?? new List<MenuCreateViewModel.MenuItem>()
            };

            return menuModel;
        }

        // Phương thức cập nhật menu
        public async Task UpdateMenuAsync(MenuCreateViewModel model, int id)
        {
            try
            {
                var menu = await _unitOfWork.Menu.GetByIdAsync(id);
                if (menu == null)
                {
                    throw new Exception("Menu not found");
                }

                var repository = _unitOfWork.Menu as GenericRepository<Menu>;

                // Upload image if provided
                if (model.Menu.ImageFile != null && model.Menu.ImageFile.Length > 0)
                {
                    var (imageUrl, thumbnailUrl) = repository.UploadImage(model.Menu.ImageFile);
                    menu.ImageUrl = imageUrl;
                    menu.ThumbnailUrl = thumbnailUrl;
                }

                // Upload icon if provided
                if (model.Menu.IconFile != null && model.Menu.IconFile.Length > 0)
                {
                    menu.Icon = repository.UploadIcon(model.Menu.IconFile);
                }

                if (model.ParentId.HasValue)
                {
                    menu.ParentId = model.ParentId.Value;
                }

                menu.Name = model.Menu.Name;
                menu.SortOrder = model.Menu.SortOrder;
                menu.PositionType = model.Menu.PositionType;
                menu.DateModified = DateTimeOffset.Now;
                // Sử dụng IHttpContextAccessor để lấy thông tin người dùng hiện tại
                var username = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
                menu.UpdateBy = username ?? "Unknown";

                await _unitOfWork.Menu.UpdateAsync(menu);
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

                // Log lỗi chi tiết
                _logger.LogError(ex, "An error occurred in {MethodName} at {Time}. Stack Trace: {StackTrace}",
                    nameof(UpdateMenuAsync), vietnamTime, ex.StackTrace);

                throw new Exception("Có lỗi xảy ra khi cập nhật menu.");
            }
        }
        public async Task DeleteMenuAsync(int id)
        {
            var menu = await _unitOfWork.Menu.GetByIdAsync(id);
            if (menu == null)
            {
                throw new Exception($"Menu with id {id} not found.");
            }

            try
            {
                // Xóa menu khỏi cơ sở dữ liệu
                await _unitOfWork.Menu.RemoveAsync(menu);
                _unitOfWork.Complete();

                // Lấy tất cả các menu còn lại từ cơ sở dữ liệu
                var allMenus = await _unitOfWork.Menu.GetAllAsync();

                // Sắp xếp lại thứ tự SortOrder nếu cần
                int newSortOrder = 1;
                foreach (var item in allMenus.OrderBy(m => m.SortOrder))
                {
                    item.SortOrder = newSortOrder++;
                    await _unitOfWork.Menu.UpdateAsync(item);
                }

                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting menu with id {MenuId}", id);
                throw new Exception("An error occurred while deleting the menu.");
            }
        }
    }
}
