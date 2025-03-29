using MySql.Data.MySqlClient;

namespace BasicApi.Services.Interfaces
{
    /// <summary>
    /// Service de contruction de la chaine de connexion
    /// </summary>
    public interface IDataBaseConnectionService
    {
        /// <summary>
        ///  Récupère la chaine de connexion
        /// </summary>
        /// <returns></returns>
        public MySqlConnection GetConnection();
    }
}
