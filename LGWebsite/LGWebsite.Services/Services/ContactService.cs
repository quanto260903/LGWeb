using Domain.Entities;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace AODWebsite.Services
{
    public class ContactService : IContactService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IUnitOfWork unitOfWork, ILogger<ContactService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _unitOfWork.Contact.GetAllAsync();
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            return await _unitOfWork.Contact.GetByIdAsync(id);
        }

        public async Task<bool> AddContactAsync(Contact contact)
        {
            try
            {
                contact.DateCreated = DateTimeOffset.Now;
                contact.DateModified = DateTimeOffset.Now;

                await _unitOfWork.Contact.AddAsync(contact);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(AddContactAsync));
                return false;
            }
        }

        public async Task<bool> UpdateContactAsync(Contact contact, string updatedBy)
        {
            try
            {
                contact.DateCreated = DateTimeOffset.Now;
                contact.DateModified = DateTimeOffset.Now;
                contact.UpdateBy = updatedBy;

                await _unitOfWork.Contact.UpdateAsync(contact);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(UpdateContactAsync));
                return false;
            }
        }

        public async Task<bool> DeleteContactAsync(int id)
        {
            try
            {
                var contact = await _unitOfWork.Contact.GetByIdAsync(id);
                if (contact == null) return false;

                await _unitOfWork.Contact.RemoveAsync(contact);
                _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                LogError(ex, nameof(DeleteContactAsync));
                return false;
            }
        }
        public async Task<bool> ToggleReadStatusAsync(int id)
        {
            var contact = await _unitOfWork.Contact.GetByIdAsync(id);
            if (contact != null)
            {
                // Đảo ngược trạng thái IsRead
                contact.IsRead = !contact.IsRead;
                await _unitOfWork.Contact.UpdateAsync(contact);
                _unitOfWork.Complete();
                return contact.IsRead;
            }
            return false;
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
