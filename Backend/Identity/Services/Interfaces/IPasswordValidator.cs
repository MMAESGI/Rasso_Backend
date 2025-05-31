using Identity.Models;

namespace Identity.Services.Interfaces
{
    /// <summary>
    /// Encapsulation de la validation de mot de passe par identity
    /// </summary>
    public interface IPasswordValidator
    {
        /// <summary>
        /// Valide le mot de passe d'un utilisateur
        /// </summary>
        /// <param name="user">Utilisateur</param>
        /// <param name="password">Mot de passe</param>
        /// <returns>Une liste d'erreur</returns>
        Task<IReadOnlyList<string>> ValidateAsync(User user, string password);
    }
}
