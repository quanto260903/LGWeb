using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EFCore.Repositories
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        public VideoRepository(AodwebsiteContext context) : base(context)
        {
        }
    }
}
