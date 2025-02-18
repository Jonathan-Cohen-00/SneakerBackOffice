using System;
using MySql.Data.MySqlClient;

namespace SneakerBackOffice.Services
{
    public class AuthService
    {
        private const string ConnectionString = "server=localhost;database=sneaker_db;user=root;password=";

        public static bool ValidateUser(string email, string password)
        {
            using (MySqlConnection conn = new MySqlConnection(ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT password FROM users WHERE email = @Email";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", email);
                    var storedHash = cmd.ExecuteScalar() as string;

                    return storedHash != null && BCrypt.Net.BCrypt.Verify(password, storedHash);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
