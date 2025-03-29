using BasicApi.Entity;
using MySql.Data.MySqlClient;

namespace BasicApi.Services
{
    public class MySqlService
    {
        private string _connectionString;

        public MySqlService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT id, name FROM users";

                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var user = new User
                                {
                                    Id = reader.GetInt32("id"),
                                    Name = reader.GetString("name")
                                };
                                users.Add(user);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }

            return users;
        }
    }
}
