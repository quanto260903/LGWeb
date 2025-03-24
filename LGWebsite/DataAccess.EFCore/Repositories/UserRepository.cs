using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EFCore.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(LgwebsiteContext context) : base(context)
        {
        }
        public async Task<IEnumerable<User>> GetUserAsync(string sortOrder, string searchString, int pageNumber, int pageSize)
        {
            var query = _context.Users.AsQueryable();

            // Lọc theo searchString
            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c => c.UserName.Contains(searchString));
            }

            // Sắp xếp theo sortOrder
            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(c => c.UserName);
                    break;
                default:
                    query = query.OrderBy(c => c.UserName);
                    break;
            }

            // Phân trang
            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public void UpdatePassword(User user, string hashedPassword)
        {

            user.PassWord = hashedPassword;
            _context.Users.Update(user);
        }
    }
}