namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IConfigurationRepository Configuration { get; }
        ISlideRepository Slide { get; }
        ICategoryRepository Category { get; }
        IPartnerRepository Partner { get; }

        IContactRepository Contact { get; }
        IMenuRepository Menu { get; }
        IUserRepository User { get; }
        ISystemConfigRepository SystemConFig { get; }
        IBlogsCategoryRepository BlogsCategory { get; }
        IVideoRepository Video { get; }
        int Complete();
    }
}
