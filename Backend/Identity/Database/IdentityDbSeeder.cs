using Common.Models;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Database
{
    public static class IdentityDbSeeder
    {
        public static async Task SeedRolesAsync(RoleManager<Role> roleManager)
        {
            foreach (var roleName in Enum.GetNames(typeof(UserRoleEnum)))
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new Role { Name = roleName });
                }
            }
        }
    }
}
