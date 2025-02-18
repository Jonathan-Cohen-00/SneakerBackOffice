using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace SneakerBackOffice.Services
{
    public class DatabaseService
    {
        private const string ConnectionString = "server=localhost;database=sneaker_db;user=root;password=your_password;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public static DataTable GetSneakers()
        {
            DataTable sneakersTable = new DataTable();

            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "SELECT id, name, brand, price FROM sneakers";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                    {
                        adapter.Fill(sneakersTable);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Database Error: " + ex.Message);
                }
            }

            return sneakersTable;
        }

        public static bool AddSneaker(string name, string brand, double price)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO sneakers (name, brand, price) VALUES (@name, @brand, @price)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@price", price);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Insert Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool UpdateSneaker(int id, string name, string brand, double price)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE sneakers SET name = @name, brand = @brand, price = @price WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@brand", brand);
                        cmd.Parameters.AddWithValue("@price", price);

                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Update Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static bool DeleteSneaker(int id)
        {
            using (var conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM sneakers WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        return cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Delete Error: " + ex.Message);
                    return false;
                }
            }
        }
    }
}
