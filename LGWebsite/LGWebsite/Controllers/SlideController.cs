using LGWebsite.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
public class SlideController : Controller
{
    private readonly ISlideService _slideService;
    private readonly ILogger<SlideController> _logger;

    public SlideController(ISlideService slideService, ILogger<SlideController> logger)
    {
        _slideService = slideService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            var slides = await _slideService.GetAllSlidesAsync();
            ViewBag.Slides = slides;
            return View();
        }
        catch (Exception ex)
        {
            LogErrorWithUtcTime(ex, nameof(Index));
            ModelState.AddModelError("", "Đã xảy ra lỗi khi tải danh sách slide. Vui lòng thử lại.");
            return View(new List<Slide>());
        }
    }

    [HttpGet]
    public IActionResult Create() => View(new Slide());

    [HttpPost]
    public async Task<IActionResult> Create(Slide data)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _slideService.CreateSlideAsync(data);
                TempData["Success"] = "Thêm slide thành công!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, nameof(Create));
                ModelState.AddModelError("", "Có lỗi xảy ra khi thêm slide. Vui lòng thử lại.");
            }
        }

        return View(data);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var slide = await _slideService.GetSlideByIdAsync(id);
            if (slide == null) return NotFound();

            return View(slide);
        }
        catch (Exception ex)
        {
            LogErrorWithUtcTime(ex, nameof(Edit));
            return View(new Slide());
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Slide model, int id)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _slideService.UpdateSlideAsync(model, id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogErrorWithUtcTime(ex, nameof(Edit));
                ModelState.AddModelError("", "Có lỗi xảy ra khi chỉnh sửa slide. Vui lòng thử lại.");
            }
        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _slideService.DeleteSlideAsync(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            LogErrorWithUtcTime(ex, nameof(Delete));
            ModelState.AddModelError("", "Đã xảy ra lỗi khi xóa slide. Vui lòng thử lại.");
            return RedirectToAction("Index");
        }
    }

  

    [HttpPost]
    public async Task<IActionResult> UpdateSlideStatus(int id, bool isEnabled)
    {
        try
        {
            var result = await _slideService.UpdateSlideStatusAsync(id, isEnabled);
            return Json(new { success = result });
        }
        catch (Exception ex)
        {
            LogErrorWithUtcTime(ex, nameof(UpdateSlideStatus));
            return Json(new { success = false, message = "Có lỗi xảy ra khi cập nhật trạng thái slide." });
        }

    }
    private void LogErrorWithUtcTime(Exception ex, string actionName)
    {
        var utcTime = DateTime.Now;
        _logger.LogError(ex, "An error occurred in {ActionName} at {Time} (UTC). Stack Trace: {StackTrace}",
            actionName, utcTime, ex.StackTrace);
    }
}
