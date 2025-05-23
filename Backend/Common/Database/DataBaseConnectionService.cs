
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
            return new MySqlConnection(GetConnectionString()); ;
        }

        public static string GetConnectionString()
        {
            string envPath = Path.Combine(Directory.GetCurrentDirectory(), "../.env");

            if (Path.Exists(envPath))
            {
                Console.WriteLine("Le fichier .env a été trouvé à l'emplacement : {envPath}.");
                Env.Load(envPath);
            }
            else
            {
                Console.WriteLine("Le fichier .env n'a pas été trouvé à l'emplacement : {envPath}.");
                Env.Load();
            }

            string? suffix = Environment.GetEnvironmentVariable($"ENV_SUFFIX");
            suffix ?= $"_{suffix}" : suffix = string.Empty;
            string? server = Environment.GetEnvironmentVariable($"DB_SERVER{suffix}");
            string? database = Environment.GetEnvironmentVariable($"DB_NAME{suffix}");
            string? port = Environment.GetEnvironmentVariable($"DB_PORT{suffix}");
            string? username = Environment.GetEnvironmentVariable($"DB_USER{suffix}");
            string? password = Environment.GetEnvironmentVariable($"DB_PASS{suffix}");


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
