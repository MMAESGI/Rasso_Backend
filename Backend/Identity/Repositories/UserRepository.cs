using Identity.Database;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public UserRepository(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public User? GetById(Guid id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }
        public User? GetByEmail(string email)
        {
            return _context.Users
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
