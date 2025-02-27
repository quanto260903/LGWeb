using Domain.Entities;

namespace AODWebsite.Services
{
    public interface IContactService
    {
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<Contact> GetContactByIdAsync(int id);
        Task<bool> AddContactAsync(Contact contact);
        Task<bool> UpdateContactAsync(Contact contact, string updatedBy);
        Task<bool> DeleteContactAsync(int id);
        Task<bool> ToggleReadStatusAsync(int id);
    }
}
