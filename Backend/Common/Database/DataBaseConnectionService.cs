
using DotNetEnv;
using MySql.Data.MySqlClient;

namespace Common.Database
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
            return new MySqlConnection(GetConnectionString()); 
        }

        public static string GetConnectionString()
        {
            string envPath = Path.Combine(Directory.GetCurrentDirectory(), "..\\.env");

            if (Path.Exists(envPath))
            {
                Console.WriteLine($"Le fichier .env n'a pas été trouvé à l'emplacement : {envPath}.");
                Env.Load(envPath);
            }
            else
            {
                Console.WriteLine($"Le fichier .env n'a pas été trouvé à l'emplacement : {envPath}.");
                Env.Load();
            }

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
                throw new InvalidDatabaseConnectionException("La chaine de connexion n'a pas pu être construite correctement avec les variables d'environnements.");
            }
            Console.WriteLine("Chaine de connexion : {server.Trim()};{port.Trim()};{database.Trim()};{username.Trim()};{password.Trim()}");

            return $"Server={server.Trim()};Port={port.Trim()};Database={database.Trim()};Uid={username.Trim()};Pwd={password.Trim()};";
        }
    }
}
