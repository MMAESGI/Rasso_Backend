using Identity.Models;

namespace Identity.Repositories
{
    /// <summary>
    /// Service d'accès à la base de données pour les utilisateurs
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Récupère un utilisateur par son email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        User? GetByEmail(string email);

        User? GetById(Guid id);

        /// <summary>
        /// Indique si l'utilisateur existe déjà
        /// </summary>
        /// <param name="email"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        Task<bool> UserExistsAsync(string email, string username);

        /// <summary>
        /// Ajoute un utilisateur à la base de données
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Le nombre d'enregistrement modifiés</returns>
        Task<int> AddUserAsync(User user);

    }
}
