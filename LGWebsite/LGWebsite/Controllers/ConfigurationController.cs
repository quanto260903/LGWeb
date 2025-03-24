using LGWebsite.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace LGWebsite.Controllers
{
    [Authorize]
    public class ConfigurationController : BaseController
    {
        private readonly ILogger<ConfigurationController> _logger;
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(ILogger<ConfigurationController> logger, IConfigurationService configurationService)
        {
            _logger = logger;
            _configurationService = configurationService;
        }

        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
                ViewBag.CurrentFilter = searchString;
                var pagedConfigurations = await _configurationService.GetAllConfigurationsAsync(sortOrder, currentFilter, searchString, page);
                return View(pagedConfigurations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index method.");
                ModelState.AddModelError("", "Error retrieving configurations.");
                return View();
            }
        }

        public IActionResult Add()
        {
            return View();
        }

        // POST: Configuration/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(WebConfiguration model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _configurationService.IsConfigKeyExistsAsync(model.ConfigKey))
                    {
                        TempData["Message"] = "ConfigKey already exists. Please enter a different value.";
                        return View(model); // Return the view without redirecting
                    }
                    // Add the new configuration
                    await _configurationService.AddConfigurationAsync(model);
                    return RedirectToAction(nameof(Index)); // Redirect to the Index action
                }
                catch (Exception ex)
                {
                    var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                    var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

                    // Log lỗi chi tiết bao gồm tên hàm, stack trace và thời gian
                    _logger.LogError(ex, "An error occurred in {ActionName} at {Time}. Stack Trace: {StackTrace}",
                        nameof(Add), vietnamTime, ex.StackTrace);

                    ModelState.AddModelError("", "Có lỗi xảy ra khi thêm liên hệ. Vui lòng thử lại.");
                }
            }
            return View(model); // Return the view with the model to display validation errors
        }

        // GET: Configuration/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var configuration = await _configurationService.GetConfigurationByIdAsync(id);
            if (configuration == null)
            {
                return NotFound();
            }
            return View(configuration);
        }

        // POST: Configuration/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WebConfiguration model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (await _configurationService.IsConfigKeyExistsAsync(model.ConfigKey, model.Id))
                    {
                        ModelState.AddModelError("ConfigKey", "ConfigKey already exists. Please enter a different value.");
                        TempData["Message"] = "ConfigKey already exists. Please enter a different value.";
                        return View(model);
                    }
                    var updatedBy = HttpContext.User.Identity.Name;

                    // Gọi service để cập nhật cấu hình
                    await _configurationService.UpdateConfigurationAsync(model, updatedBy);
                    TempData["Message"] = "Config updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating configuration."); // Log the error
                    var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                    var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);

                    // Log lỗi chi tiết bao gồm tên hàm, stack trace và thời gian
                    _logger.LogError(ex, "An error occurred in {ActionName} at {Time}. Stack Trace: {StackTrace}",
                        nameof(Edit), vietnamTime, ex.StackTrace);

                    ModelState.AddModelError("", "Có lỗi xảy ra khi thêm liên hệ. Vui lòng thử lại.");
                }
            }
            return View(model); // Return the view with validation errors
        }

  
        // POST: Configuration/Delete/{id}
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _configurationService.DeleteConfigurationAsync(id); // Delete the configuration
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting configuration."); // Log the error
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa cấu hình."); // Optional: Add a user-friendly error message
                return RedirectToAction(nameof(Index)); // Redirect to Index if error occurs
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
                var pagedConfigurations = await _configurationService.GetAllConfigurationsAsync(sortOrder, currentFilter, searchString, page);
                return PartialView("_ConfigurationList", pagedConfigurations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index method.");
                ModelState.AddModelError("", "Error retrieving configurations.");
                return PartialView("_ConfigurationList", new StaticPagedList<WebConfiguration>(new List<WebConfiguration>(), page ?? 1, 0, 0));
            }
        }
    }
}
