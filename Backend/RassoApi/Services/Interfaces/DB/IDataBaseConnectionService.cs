using MySql.Data.MySqlClient;

namespace RassoApi.Services.Interfaces.DB
{
    /// <summary>
    /// Service de contruction de la chaine de connexion
    /// </summary>
    public interface IDataBaseConnectionService
    {
        /// <summary>
        ///  Récupère la connexion à la base de données
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection();

        /// <summary>
        /// Retourne la chaine de connexion à la base de données
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString();
    }
}
