using Identity.Database;
using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }


        public User? GetById(Guid id)
        {
            return _context.Users
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                    .FirstOrDefault(x => x.Id == id);
        }
        public User? GetByEmail(string email)
        {
            return _context.Users
                  .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefault(u => u.Email == email && u.IsActive);
        }

        public async Task<bool> UserExistsAsync(string email, string username)
        {
            return await _context.Users
                .AnyAsync(u => u.Email == email || u.UserName == username);
        }

        public async Task<int> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            return await _context.SaveChangesAsync();
        }
    }
}
