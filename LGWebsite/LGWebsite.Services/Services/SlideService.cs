using DataAccess.EFCore.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace LGWebsite.Services
{
    public class SlideService : ISlideService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SlideService> _logger;

        public SlideService(IUnitOfWork unitOfWork, ILogger<SlideService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Slide>> GetAllSlidesAsync()
        {
            return await _unitOfWork.Slide.GetAllAsync();
        }

        public async Task<Slide> GetSlideByIdAsync(int id)
        {
            return await _unitOfWork.Slide.GetByIdAsync(id);
        }

        public async Task CreateSlideAsync(Slide slide)
        {
            if (slide.ImageFile != null && slide.ImageFile.Length > 0)
            {
                var repository = _unitOfWork.Slide as GenericRepository<Slide>;
                var (imageUrl, thumbnailUrl) = repository.UploadImage(slide.ImageFile);
                slide.ImageUrl = imageUrl;
                slide.ThumbnailUrl = thumbnailUrl;
            }

            slide.DateCreated = DateTimeOffset.Now;
            slide.DateModified = DateTimeOffset.Now;

            await _unitOfWork.Slide.AddAsync(slide);
            _unitOfWork.Complete();
        }

        public async Task UpdateSlideAsync(Slide slide, int id)
        {
            var existingSlide = await _unitOfWork.Slide.GetByIdAsync(id);
            if (existingSlide == null) throw new Exception("Slide not found");

            if (slide.ImageFile != null && slide.ImageFile.Length > 0)
            {
                var repository = _unitOfWork.Slide as GenericRepository<Slide>;
                var (imageUrl, thumbnailUrl) = repository.UploadImage(slide.ImageFile);
                existingSlide.ImageUrl = imageUrl;
                existingSlide.ThumbnailUrl = thumbnailUrl;
            }

            existingSlide.Url = slide.Url;
            existingSlide.IsEnabled = slide.IsEnabled;
            existingSlide.DateModified = DateTimeOffset.Now;

            _unitOfWork.Complete();
        }

        public async Task DeleteSlideAsync(int id)
        {
            var slide = await _unitOfWork.Slide.GetByIdAsync(id);
            if (slide == null) throw new Exception("Slide not found");

            await _unitOfWork.Slide.RemoveAsync(slide);
            _unitOfWork.Complete();
        }

        public async Task<bool> UpdateSlideStatusAsync(int id, bool isEnabled)
        {
            try
            {
                var Slide = await _unitOfWork.Slide.GetByIdAsync(id);
                if (Slide != null)
                {
                    Slide.IsEnabled = isEnabled;
                    await _unitOfWork.Slide.UpdateAsync(Slide);
                    _unitOfWork.Complete();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật trạng thái Slide");
                return false;
            }
        }
    }
}
