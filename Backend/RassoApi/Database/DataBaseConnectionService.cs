using RassoApi.Exceptions;
using MySql.Data.MySqlClient;
using RassoApi.Services.Interfaces.DB;

namespace RassoApi.Database
{
    /// <inheritdoc cref="IDataBaseConnectionService"/>
    public class DataBaseConnectionService : IDataBaseConnectionService
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public DataBaseConnectionService()
        {
        }

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(GetConnectionString()); ;
        }

        public string GetConnectionString()
        {
            string? server = Environment.GetEnvironmentVariable("DB_SERVER");
            string? database = Environment.GetEnvironmentVariable("DB_NAME");
            string? port = Environment.GetEnvironmentVariable("DB_PORT");
            string? username = Environment.GetEnvironmentVariable("DB_USER");
            string? password = Environment.GetEnvironmentVariable("DB_PASS");

            if (string.IsNullOrEmpty(server)
                || string.IsNullOrEmpty(database)
                || string.IsNullOrEmpty(port)
                || string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password))
            {
                throw new InvalidDatabaseConnectionException("La chaine de connexion n'a pas pu être construite correctement");
            }

            return $"Server={server};Port={port};Database={database};Uid={username};Pwd={password};";
        }
    }
}
