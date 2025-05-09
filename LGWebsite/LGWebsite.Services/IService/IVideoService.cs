﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGWebsite.Services.IService
{
    public interface IVideoService
    {
        Task<List<Video>> GetVideosByBlogCategoryIdAsync(int blogcategoryId);
        Task<Video> GetVideoByIdAsync(int videoId);
    }
}
