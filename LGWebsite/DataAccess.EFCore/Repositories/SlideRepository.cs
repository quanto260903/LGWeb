using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class SlideRepository : GenericRepository<Slide>, ISlideRepository
    {
        public SlideRepository(AodwebsiteContext context) : base(context)
        {
        }

    }
}
