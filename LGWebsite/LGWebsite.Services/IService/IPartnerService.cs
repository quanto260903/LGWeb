
using Domain.Entities;

namespace AODWebsite.Services
{
    public interface IPartnerService
    {
        Task<IEnumerable<Partner>> GetAllPartnersAsync();
        Task<Partner> GetPartnerByIdAsync(int id);
        Task<bool> AddPartnerAsync(Partner model);
        Task<bool> EditPartnerAsync(int id, Partner model);
        Task<bool> DeletePartnerAsync(int id);
    }
}
