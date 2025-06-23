
using Common.Models;
using Identity.Database;
using Identity.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddRoleAsync(string roleName)
        {
            if (await RoleExistsAsync(roleName))
                return;

            var role = new Role
            {
                Name = roleName
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<string>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Where(r => r.Name != null)
                .Select(r => r.Name!)
                .ToListAsync();
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _context.Roles.AnyAsync(r => r.Name == roleName);
        }

        public async Task<Role?> GetRoleByName(UserRoleEnum roleEnum)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == roleEnum.ToString());
        }
    }
}
