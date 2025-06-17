using Identity.Models;

namespace Identity.Services.Interfaces
{
    /// <summary>
    /// Encapsulation de la gestion des mots de passe effectuée par ASP.NET Identity
    /// </summary>
    public interface IPasswordManager
    {
        /// <summary>
        /// Valide le mot de passe d'un utilisateur
        /// </summary>
        /// <param name="user">Utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <returns>Une liste d'erreur</returns>
        Task<IReadOnlyList<string>> ValidateAsync(User user, string password);

        /// <summary>
        /// Hache le mot de passe d'un utilisateur
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string HashPassword(User user, string password);
    }
}
