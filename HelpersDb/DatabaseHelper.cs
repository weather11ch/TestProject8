using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System.Reflection.PortableExecutable;
using TestProject8.ModelsDb;

namespace TestProject8.HelpersDb
{
    public class DatabaseHelper
    {
        private string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void CreateNewRow(string name, string email)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"INSERT INTO users (Name, Email) VALUES ('{name}','{email}');";
                
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Email", email);
                    command.ExecuteNonQueryAsync();
                }
            }
        }

        public List<User> GetUsers(string name)
        {
            var users = new List<User>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = $"SELECT Id, Name, Email FROM users where Name = '{name}';";

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Email = reader.GetString("Email")
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
    }
}
