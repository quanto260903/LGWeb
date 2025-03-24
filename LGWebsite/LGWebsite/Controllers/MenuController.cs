using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using DataAccess.EFCore.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using LGWebsite.Services.IService;

namespace LGWebsite.Controllers
{
    [Authorize]
    public class MenuController : BaseController
    {
        private readonly IMenuService _menuService;
        private readonly ILogger<MenuController> _logger;

        public MenuController(ILogger<MenuController> logger, IMenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        public async Task<IActionResult> Index()
        {
            var menus = await _menuService.GetAllMenuAsync();
            return View(menus);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _menuService.MenuCreateViewModelAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuCreateViewModel data)
        {
            if (ModelState.IsValid)
            {
                var result = await _menuService.CreateMenuAsync(data);
                if (result)
                {
                    TempData["Success"] = "Thêm Menu thành công!";
                    return RedirectToAction("Index");
                }
            }

            // Restore MenuItems in case of failure
            data = await _menuService.MenuCreateViewModelAsync();
            return View(data);
       
    }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var menuModel = await _menuService.MenuEditAsync(id);
            if (menuModel == null)
            {
                return NotFound();
            }

            return View(menuModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(MenuCreateViewModel model, int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _menuService.UpdateMenuAsync(model, id);
                    TempData["Success"] = "Cập nhật Menu thành công!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Có lỗi xảy ra khi cập nhật menu.");
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật menu. Vui lòng thử lại.");
                }
            }

            return View(model);
    }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _menuService.DeleteMenuAsync(id); // Gọi service để xóa menu
                TempData["Success"] = "Xóa Menu thành công!";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting menu with id {MenuId}", id);
                TempData["Error"] = "Có lỗi xảy ra khi xóa Menu.";
                return StatusCode(500, "An error occurred while deleting the menu.");
            }

            return RedirectToAction("Index");
        }
    }
}