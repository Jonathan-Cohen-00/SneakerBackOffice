using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SneakerBackOffice.Models;
using System;

namespace SneakerBackOffice.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; } // Assure-toi que la table `Users` existe

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("⚠️ La chaîne de connexion est vide !");
                }

                Console.WriteLine("✅ Connexion à MySQL avec : " + connectionString);

                optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 23)));
            }
        }

        public bool TestDatabaseConnection()
        {
            try
            {
                // 🔥 Vérifie si la table Users est accessible
                int userCount = Users.Count();
                Console.WriteLine($"🎉 Connexion OK ! Nombre d'utilisateurs : {userCount}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erreur de connexion : {ex.Message}");
                return false;
            }
        }
    }
}
