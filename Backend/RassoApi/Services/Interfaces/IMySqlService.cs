using RassoApi.Entity;

namespace RassoApi.Services.Interfaces
{
    /// <summary>
    /// Interface pour le POC Jwt
    /// </summary>
    public interface IMySqlService
    {
        /// <summary>
        /// Récupère la liste des utilisateurs
        /// </summary>
        /// <returns></returns>
        public List<User> GetUsers();
    }
}
