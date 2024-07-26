using MySql.Data.MySqlClient;
using System.Data;
using TestProject8.HelpersDb;
using TestProject8.ModelsDb;

namespace YourNamespace
{
    [TestFixture]
    public class DatabaseTests
    {
        string connectionString = "Server=localhost;Database=testbase;User Id=testuser;Password=Record7422;";

        [Test]
        public void TestDatabaseConnection()
        {
            var connection = new MySqlConnection(connectionString);
            using (connection)
            {
                connection.Open();
                Assert.That(connection.State, Is.EqualTo(ConnectionState.Open));
            }
        }

       // [TestCase("John Doe", "john.doe@example.com")]
        //[TestCase("Jack Doe", "jack.doe@example.com")]
        //public void TestGetUsers(string name, string email)
        //{
        //    var dbHelper = new DatabaseHelper(connectionString);
        //   // List<User> users = dbHelper.GetUsers();            

        //    foreach (var user in users)
        //    {
        //        Assert.That(user.Name.Equals(name));
        //        Assert.That(user.Email.Equals(email));
        //    }
        //}

        [Test]
        public void TestGetAllColumns()
        {
            string query = "SELECT * FROM users"; 

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new MySqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Собираем строку из всех столбцов
                            var rowString = string.Empty;
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                rowString += $"{reader.GetName(i)}: {reader[i]} ";
                            }
                            
                            Assert.IsNotEmpty(rowString); 
                        }
                    }
                }
            }
        }
        [Test]
        public void TestInsert()
        {
            var dbHelper = new DatabaseHelper(connectionString);
            string newName = "Jack";
            string newEmail = "jack@mail.com";
            dbHelper.CreateNewRow(newName, newEmail);
            List<User> users = dbHelper.GetUsers(newName);
            Assert.That(users.FirstOrDefault().Name, Is.EqualTo(newName));
        }

    }  
}