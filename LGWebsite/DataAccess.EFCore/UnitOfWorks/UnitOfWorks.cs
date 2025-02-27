using Domain.Interfaces;
using DataAccess.EFCore.Repositories;
using Domain.Entities;

namespace DataAccess.EFCore.UnitOfWorks
{
    public class UnitOfWorks : IUnitOfWork
    {
        private readonly AodwebsiteContext _context;
        public IConfigurationRepository Configuration { get; private set; }
        public ISlideRepository Slide { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IPartnerRepository Partner { get; private set; }
        public IContactRepository Contact { get; private set; }
        public IMenuRepository Menu { get; private set; }
        public IUserRepository User { get; private set; }
        public ISystemConfigRepository SystemConFig { get; private set; }
        public IBlogsCategoryRepository BlogsCategory { get; private set; }

        public IVideoRepository Video { get; private set; }

        public UnitOfWorks(AodwebsiteContext context)
        {
            _context = context;
            Configuration = new ConfigurationRepository(_context);
            Slide = new SlideRepository(_context);
            Category = new CategoryRepository(_context);
            Partner = new PartnerRepository(_context);
            Contact = new ContactRepository(_context);
            Menu = new MenuRepository(_context);
            User = new UserRepository(_context);
            SystemConFig = new SystemConFigRepository(_context);
            BlogsCategory = new BlogsCategoryRepository(_context);
            Video = new VideoRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
