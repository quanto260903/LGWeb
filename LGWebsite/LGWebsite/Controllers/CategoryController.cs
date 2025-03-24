using Domain.Entities;
using LGWebsite.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LGWebsite.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryService.GetAllCategoriesAsync();
                return View(categories);
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, nameof(GetCategories));
                return View("Error");
            }
        }

        public IActionResult Add()
        {
            return View(new Category());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.AddCategoryAsync(model);
                    TempData["Success"] = "Thêm category thành công!";
                    return RedirectToAction(nameof(GetCategories));
                }
                catch (Exception ex)
                {
                    LogErrorWithUtcTime(ex, nameof(Add));
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi thêm category.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return NotFound();
                }
                return View(category);
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, $"Error fetching category with id {id}.");
                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategoryAsync(model, id);
                    TempData["Success"] = "Cập nhật category thành công!";
                    return RedirectToAction(nameof(GetCategories));
                }
                catch (Exception ex)
                {
                    LogErrorWithUtcTime(ex, nameof(Edit));
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi cập nhật category.");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                TempData["Success"] = "Xóa category thành công!";
                return RedirectToAction(nameof(GetCategories));
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, nameof(Delete));
                TempData["Error"] = "Đã xảy ra lỗi khi xóa category.";
                return RedirectToAction(nameof(GetCategories));
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategoryStatus(int id, bool isEnabled)
        {
            try
            {
                await _categoryService.UpdateCategoryStatusAsync(id, isEnabled);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, nameof(UpdateCategoryStatus));
                return Json(new { success = false });
            }
        }

        private void LogErrorWithUtcTime(Exception ex, string actionName)
        {
            var utcTime = DateTime.Now;
            _logger.LogError(ex, "An error occurred in {ActionName} at {Time} (UTC). Stack Trace: {StackTrace}",
                actionName, utcTime, ex.StackTrace);
        }
    }
}
