using LGWebsite.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace LGWebsite.Controllers
{
    [Authorize]
    public class ContactController : BaseController
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService contactService, ILogger<ContactController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var contacts = await _contactService.GetAllContactsAsync();
                ViewBag.Contacts = contacts;
                return View();
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, nameof(Index));
                ModelState.AddModelError("", "Đã xảy ra lỗi khi tải danh sách liên hệ. Vui lòng thử lại.");
                return View(new List<Contact>());
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isSuccess = await _contactService.AddContactAsync(data);
                    if (isSuccess)
                    {
                        TempData["Message"] = "Thêm liên hệ thành công!";
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", "Có lỗi xảy ra khi thêm liên hệ. Vui lòng thử lại.");
                }
                catch (Exception ex)
                {
                    LogErrorWithUtcTime(ex, nameof(Create));
                    ModelState.AddModelError("", "Có lỗi xảy ra khi thêm liên hệ. Vui lòng thử lại.");
                }
            }

            return View(data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var contact = await _contactService.GetContactByIdAsync(id);
                if (contact == null) return NotFound();

                return View(contact);
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, nameof(Edit));
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedBy = HttpContext.User.Identity.Name;
                    bool isSuccess = await _contactService.UpdateContactAsync(model, updatedBy);
                    if (isSuccess)
                    {
                        TempData["Message"] = "Cập nhật Contact thành công!";
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật Contact. Vui lòng thử lại.");
                }
                catch (Exception ex)
                {
                    LogErrorWithUtcTime(ex, nameof(Edit));
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật Contact. Vui lòng thử lại.");
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool isSuccess = await _contactService.DeleteContactAsync(id);
                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa Contact. Vui lòng thử lại.");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, nameof(Delete));
                ModelState.AddModelError("", "Có lỗi xảy ra khi xóa Contact. Vui lòng thử lại.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleReadStatus(int id)
        {
            var isRead = await _contactService.ToggleReadStatusAsync(id);

            return Json(new { success = true, isRead });
        }

        private void LogErrorWithUtcTime(Exception ex, string actionName)
        {
            var utcTime = DateTime.Now;
            _logger.LogError(ex, "An error occurred in {ActionName} at {Time} (UTC). Stack Trace: {StackTrace}",
                actionName, utcTime, ex.StackTrace);
        }
    }
}
