using DataAccess.EFCore.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AODWebsite.Services
{
    public class PartnerService : IPartnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PartnerService> _logger;

        public PartnerService(IUnitOfWork unitOfWork, ILogger<PartnerService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Partner>> GetAllPartnersAsync()
        {
            return await _unitOfWork.Partner.GetAllAsync();
        }

        public async Task<Partner> GetPartnerByIdAsync(int id)
        {
            return await _unitOfWork.Partner.GetByIdAsync(id);
        }

        public async Task<bool> AddPartnerAsync(Partner model)
        {
            try
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var repository = _unitOfWork.Menu as GenericRepository<Menu>;
                    var (imageUrl, thumbnailUrl) = repository.UploadImage(model.ImageFile);

                    model.ImageUrl = imageUrl;
                    model.ThumbnailUrl = thumbnailUrl;
                }

                model.DateCreated = DateTimeOffset.Now;
                model.DateModified = DateTimeOffset.Now;
                await _unitOfWork.Partner.AddAsync(model);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, "AddPartnerAsync");
                return false;
            }
        }

        public async Task<bool> EditPartnerAsync(int id, Partner model)
        {
            try
            {
                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    var repository = _unitOfWork.Menu as GenericRepository<Menu>;
                    var (imageUrl, thumbnailUrl) = repository.UploadImage(model.ImageFile);

                    model.ImageUrl = imageUrl;
                    model.ThumbnailUrl = thumbnailUrl;
                }

                var existingPartner = await _unitOfWork.Partner.GetByIdAsync(id);
                if (existingPartner == null)
                {
                    return false;
                }

                existingPartner.ImageUrl = model.ImageUrl;
                existingPartner.DateModified = DateTimeOffset.UtcNow;
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, "EditPartnerAsync");
                return false;
            }
        }

        public async Task<bool> DeletePartnerAsync(int id)
        {
            try
            {
                var partner = await _unitOfWork.Partner.GetByIdAsync(id);
                if (partner == null)
                {
                    return false;
                }

                await _unitOfWork.Partner.RemoveAsync(partner);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, "DeletePartnerAsync");
                return false;
            }
        }

        private void LogError(Exception ex, string methodName)
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone);
            _logger.LogError(ex, "An error occurred in {MethodName} at {Time}. Stack Trace: {StackTrace}",
                methodName, vietnamTime, ex.StackTrace);
        }
    }

}
