using Domain.Entities;

namespace AODWebsite.Services
{
    public interface ISlideService
    {
        Task<IEnumerable<Slide>> GetAllSlidesAsync();
        Task<Slide> GetSlideByIdAsync(int id);
        Task CreateSlideAsync(Slide slide);
        Task UpdateSlideAsync(Slide slide, int id);
        Task DeleteSlideAsync(int id);
        Task<bool> UpdateSlideStatusAsync(int id, bool isEnabled);
    }
}
