using Domain.Models;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using DataAccess.EFCore.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LGWebsite.Services;
using Microsoft.Data.SqlClient;
using X.PagedList;

namespace LGWebsite.Controllers
{
    public class SystemConFigController : BaseController
    {
        private readonly ISystemConfigService _systemConfigService;
        private readonly ILogger<SystemConFigController> _logger;

        public SystemConFigController(ISystemConfigService systemConfigService, ILogger<SystemConFigController> logger)
        {
            _systemConfigService = systemConfigService;
            _logger = logger;
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
                var pagedConfigurations = await _systemConfigService.GetAllConfigurationsAsync(sortOrder, currentFilter, searchString, page);
                return View(pagedConfigurations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index method.");
                ModelState.AddModelError("", "Error retrieving configurations.");
                return View();
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
                var pagedConfigurations = await _systemConfigService.GetAllConfigurationsAsync(sortOrder, currentFilter, searchString, page);
                return PartialView("_ConfigurationList", pagedConfigurations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index method.");
                ModelState.AddModelError("", "Error retrieving configurations.");
                return PartialView("_ConfigurationList", new StaticPagedList<SystemConfiguration>(new List<SystemConfiguration>(), page ?? 1, 0, 0));
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SystemConfiguration model)
        {
            if (ModelState.IsValid)
            {
                if (await _systemConfigService.IsConfigKeyExistsAsync(model.ConfigKey))
                {
                    TempData["Message"] = "ConfigKey already exists. Please enter a different value.";
                    return View(model); // Return the view without redirecting
                }

                await _systemConfigService.AddConfigAsync(model);
                TempData["Message"] = "Config created successfully!";
                return RedirectToAction("Index");
            }
        
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var config = await _systemConfigService.GetConfigByIdAsync(id);
            if (config == null) return NotFound();
            return View(config);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SystemConfiguration model)
        {
            if (ModelState.IsValid)
            {
                if (ModelState.IsValid)
                {
                    if (await _systemConfigService.IsConfigKeyExistsAsync(model.ConfigKey, model.Id))
                    {
                        ModelState.AddModelError("ConfigKey", "ConfigKey already exists. Please enter a different value.");
                        TempData["Message"] = "ConfigKey already exists. Please enter a different value.";
                        return View(model);
                    }
                    var updatedBy = HttpContext.User.Identity.Name;
                    await _systemConfigService.UpdateConfigAsync(model, updatedBy);
                    TempData["Message"] = "Config updated successfully!";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _systemConfigService.DeleteConfigAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting SystemConfig");
                return RedirectToAction("Index");
            }
        }
    }

}