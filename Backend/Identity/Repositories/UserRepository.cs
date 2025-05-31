using Identity.Database;
using Identity.Models;

namespace Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public User? GetByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(u => u.Email == email && u.IsActive);
        }
    }
}
