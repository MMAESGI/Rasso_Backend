using Common.Results;
using Identity.Models;

namespace Identity.Services.Interfaces
{
    /// <summary>
    /// Service de gestion des utilisateurs
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Indique si les identifiants sont valides
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Mot de passe</param>
        /// <returns></returns>
        Task<Result<User>> GetUser(string email, string password);


        /// <summary>
        /// Indique si les identifiants sont valides
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Mot de passe</param>
        /// <returns></returns>
        Task<Result<User>> RegisterUser(string email, string password);
    }
}
