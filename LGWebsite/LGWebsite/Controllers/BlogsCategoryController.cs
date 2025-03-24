using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using LGWebsite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using X.PagedList;

namespace LGWebsite.Controllers
{
    public class BlogsCategoryController : BaseController
    {
        private readonly IBlogsCategoryService _blogsCategoryService;
        private readonly ILogger<BlogsCategoryController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public BlogsCategoryController(IBlogsCategoryService blogsCategoryService, IUnitOfWork unitOfWork, ILogger<BlogsCategoryController> logger)
        {
            _blogsCategoryService = blogsCategoryService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.CurrentFilter = searchString;
                var pagedBlogCategory = await _blogsCategoryService.GetAllBlogsCategoryAsync(sortOrder, currentFilter, searchString, page);
                return View(pagedBlogCategory);
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, "Index");
                return View("Error"); // Redirect to a custom error view
            }
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _unitOfWork.Category.GetAllAsync();
            ViewBag.Categories = categories;
            var position = await _unitOfWork.BlogsCategory.GetAllAsync();
            ViewBag.BlogsCategory = position;
            return View(); // Initialize a new BlogsCategory model for the view
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.Features = Request.Form["FeaturesCombined"];
                    await _blogsCategoryService.AddAsync(model);
                    TempData["Success"] = "Blog category created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    LogErrorWithUtcTime(ex, "Create");
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the blog category.");
                }
            }

            // Reload categories in case of an error or invalid ModelState
            ViewBag.Categories = await _unitOfWork.Category.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            try
            {
                var categories = await _unitOfWork.Category.GetAllAsync();
                ViewBag.Categories = categories;
                var blogsCategory = await _blogsCategoryService.GetByIdAsync(id);
                if (blogsCategory == null)
                {
                    return NotFound();
                }
                return View(blogsCategory);
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, "Edit");
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BlogsCategory model, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _blogsCategoryService.UpdateAsync(model);
                    TempData["Success"] = "Blog category updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    LogErrorWithUtcTime(ex, "Edit");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the blog category.");
                }
            }
            ViewBag.Categories = await _unitOfWork.Category.GetAllAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBlogsCategoryStatus(int id, bool isDeleted, bool isDraft, bool isHome)
        {
            try
            {
                await _blogsCategoryService.UpdateBlogsCategoryStatus(id, isDeleted, isDraft, isHome);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, "UpdateBlogsCategoryStatus");
                return Json(new { success = false });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _blogsCategoryService.DeleteAsync(id);
                TempData["Success"] = "Blog category deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, "Delete");
                ModelState.AddModelError(string.Empty, "An error occurred while deleting the blog category.");
                return RedirectToAction(nameof(Index));
            }
        }
        public async Task<IActionResult> GetConfigurations(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.CurrentFilter = searchString;
                var pagedConfigurations = await _blogsCategoryService.GetAllBlogsCategoryAsync(sortOrder, currentFilter, searchString, page);
                return PartialView("_BlogCategoryList", pagedConfigurations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index method.");
                ModelState.AddModelError("", "Error retrieving configurations.");
                return PartialView("_ConfigurationList", new StaticPagedList<WebConfiguration>(new List<WebConfiguration>(), page ?? 1, 0, 0));
            }
        }
        private void LogErrorWithUtcTime(Exception ex, string actionName)
        {
            var utcTime = DateTime.Now;
            _logger.LogError(ex, "An error occurred in {ActionName} at {Time} (UTC). Stack Trace: {StackTrace}", actionName, utcTime, ex.StackTrace);
        }
    }
}
