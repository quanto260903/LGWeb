using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using LGWebsite.Services;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Domain.Models;
using Microsoft.Data.SqlClient;
using X.PagedList;

namespace LGWebsite.Controllers
{
    [Authorize(Roles = "0")]
    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.CurrentFilter = searchString;
                var pagedConfigurations = await _userService.GetAllUsersAsync(sortOrder, currentFilter, searchString, page);
                return View(pagedConfigurations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index method.");
                ModelState.AddModelError("", "Error retrieving configurations.");
                return View();
            }
        }
        public async Task<IActionResult> GetUsers(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.CurrentFilter = searchString;
                var pagedConfigurations = await _userService.GetAllUsersAsync(sortOrder, currentFilter, searchString, page);
                return PartialView("_UserList", pagedConfigurations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index method.");
                ModelState.AddModelError("", "Error retrieving configurations.");
                return PartialView("_UserList", new StaticPagedList<User>(new List<User>(), page ?? 1, 0, 0));
            }
        }

            [HttpGet]
        public IActionResult Create()
        {
            ViewBag.RoleList = new SelectList(Enum.GetValues(typeof(UserRole)).Cast<UserRole>());
            return View(new User());
        }

        [HttpPost]
        public async Task<IActionResult> Create(User data)
        {
            try
            {
                await _userService.AddUserAsync(data);
                ViewBag.Status = true;
                ViewBag.Message = "Thêm User thành công!";
            }
            catch (Exception ex)
            {
                LogErrorWithTimestamp(ex, nameof(Create));
                ViewBag.Status = false;
                ViewBag.Message = "Có lỗi xảy ra khi thêm người dùng: " + ex.Message;
            }

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                LogErrorWithTimestamp(ex, nameof(Edit));
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(User model, int id)
        {
            try
            {
                await _userService.UpdateUserAsync(id, model);
                TempData["Status"] = true;
                TempData["Message"] = "Cập nhật User thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogErrorWithTimestamp(ex, nameof(Edit));
                TempData["Status"] = false;
                TempData["Message"] = "Cập nhật không thành công: " + ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            try
            {
                var user = await _userService.GetUserByIdAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            catch (Exception ex)
            {
                LogErrorWithTimestamp(ex, nameof(DeleteConfirmation));
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogErrorWithTimestamp(ex, nameof(Delete));
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUserStatus(int id, bool isEnabled)
        {
            try
            {
                var result = await _userService.UpdateUserStatusAsync(id, isEnabled);
                return Json(new { success = result });
            }
            catch (Exception ex)
            {
                LogErrorWithTimestamp(ex, nameof(UpdateUserStatus));
                return Json(new { success = false });
            }
        }

        private void LogErrorWithTimestamp(Exception ex, string methodName)
        {
            var currentTime = DateTimeOffset.Now.ToString("yyyy-MM-dd HH:mm:ss zzz");
            _logger.LogError(ex, $"[{currentTime}] Error in {methodName}: {ex.Message}");
        }
    }
}
