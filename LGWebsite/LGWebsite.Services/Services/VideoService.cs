using AODWebsite.Services.IService;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AODWebsite.Services.Services
{
    public class VideoService : IVideoService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<VideoService> _logger;

        public VideoService(IUnitOfWork unitOfWork, ILogger<VideoService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<List<Video>> GetVideosByBlogCategoryIdAsync(int blogcategoryId)
        {
            var videos = await _unitOfWork.Video.FindAsync(v => v.BlogCategoryId == blogcategoryId);
            return videos.ToList();
        }

        public async Task<Video> GetVideoByIdAsync(int videoId)
        {
            return await _unitOfWork.Video.GetByIdAsync(videoId);
        }
    }
}
