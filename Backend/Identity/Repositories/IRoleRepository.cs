using Common.Models;
using Identity.Models;

namespace Identity.Repositories
{
    public interface IRoleRepository
    {
        /// <summary>
        /// Checks if a role exists by its name.
        /// </summary>
        /// <param name="roleName">The name of the role to check.</param>
        /// <returns>True if the role exists, otherwise false.</returns>
        Task<bool> RoleExistsAsync(string roleName);
        /// <summary>
        /// Adds a new role to the database.
        /// </summary>
        /// <param name="roleName">The name of the role to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddRoleAsync(string roleName);

        /// <summary>
        /// Gets all roles from the database.
        /// </summary>
        /// <returns>A list of role names.</returns>
        Task<IList<string>> GetAllRolesAsync();

        /// <summary>
        /// Get role by name
        /// </summary>
        /// <returns>A list of role names.</returns>
        Task<Role?> GetRoleByName(UserRoleEnum roleEnum);
    }
}
