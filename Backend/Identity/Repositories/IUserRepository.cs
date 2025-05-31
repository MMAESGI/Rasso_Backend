using Identity.Models;

namespace Identity.Repositories
{
    /// <summary>
    /// Service d'accès à la base de données pour les utilisateurs
    /// </summary>
    public interface IUserRepository
    {
        User? GetByEmail(string email);
    }
}
