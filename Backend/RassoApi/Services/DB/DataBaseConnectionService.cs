using RassoApi.Exceptions;
using MySql.Data.MySqlClient;
using RassoApi.Services.Interfaces.DB;

namespace RassoApi.Services.DB
{
    /// <inheritdoc cref="IDataBaseConnectionService"/>
    public class DataBaseConnectionService : IDataBaseConnectionService
    {

        public DataBaseConnectionService()
        {
        }

        public MySqlConnection GetConnection()
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

            string connectionString = $"Server={server};Port={port};Database={database};Uid={username};Pwd={password};";

            return new MySqlConnection(connectionString); ;
        }
    }
}
