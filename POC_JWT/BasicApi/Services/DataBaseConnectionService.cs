using BasicApi.Exceptions;
using BasicApi.Services.Interfaces;
using MySql.Data.MySqlClient;

namespace BasicApi.Services
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

            if (String.IsNullOrEmpty(server)
                || String.IsNullOrEmpty(database)
                || String.IsNullOrEmpty(port)
                || String.IsNullOrEmpty(username)
                || String.IsNullOrEmpty(password))
            {
                throw new InvalidDatabaseConnectionException("La chaine de connexion n'a pas pu être construite correctement");
            }

            string connectionString = $"Server={server};Port={port};Database={database};Uid={username};Pwd={password};";

            return new MySqlConnection(connectionString); ;
        }
    }
}
