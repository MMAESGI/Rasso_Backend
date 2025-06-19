
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
                Console.WriteLine($"Le fichier .env a été trouvé à l'emplacement : {envPath}.");
                Env.Load(envPath);
            }
            else
            {
                Console.WriteLine($"Le fichier .env n'a pas été trouvé à l'emplacement : {envPath}.\n Recherche du .env en cours ...");
                Env.Load();
            }

            string? server = Environment.GetEnvironmentVariable("DB_SERVER")?.Trim();
            string? database = Environment.GetEnvironmentVariable("DB_NAME")?.Trim();
            string? port = Environment.GetEnvironmentVariable("DB_PORT")?.Trim();
            string? username = Environment.GetEnvironmentVariable("DB_USER")?.Trim();
            string? password = Environment.GetEnvironmentVariable("DB_PASS")?.Trim();


            if (string.IsNullOrEmpty(server)
                || string.IsNullOrEmpty(database)
                || string.IsNullOrEmpty(port)
                || string.IsNullOrEmpty(username)
                || string.IsNullOrEmpty(password))
            {
                Console.WriteLine($"Chaine de connexion en erreur : {server};{port};{database};{username};{password}");
                throw new InvalidDatabaseConnectionException($"La chaine de connexion n'a pas pu être construite correctement avec les variables d'environnements.");
            }
            Console.WriteLine($"Chaine de connexion en erreur : {server};{port};{database};{username};{password}");

            return $"Server={server};Port={port};Database={database};Uid={username};Pwd={password};";
        }
    }
}
