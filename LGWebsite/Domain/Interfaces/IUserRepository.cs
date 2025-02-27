using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetUserByEmailAsync(string email);
        void UpdatePassword(User user, string hashedPassword);
        Task<IEnumerable<User>> GetUserAsync(string sortOrder, string searchString, int pageNumber, int pageSize);

    }
}
