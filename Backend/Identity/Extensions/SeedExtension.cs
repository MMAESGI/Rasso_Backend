using Identity.Database;
using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Extensions
{
    public static class SeedExtension
    {
        /// <summary>
        /// Peuplement des données pour l'identité
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task SeedIdentityDataAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

            await IdentityDbSeeder.SeedRolesAsync(roleManager);
        }
    }
}
