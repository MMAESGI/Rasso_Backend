using MySql.Data.MySqlClient;

namespace Common.Database
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
    }
}
