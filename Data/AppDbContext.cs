using Microsoft.EntityFrameworkCore;
using SneakerBackOffice.Models;

namespace SneakerBackOffice.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=127.0.0.1;database=nike_claude;user=root;password=your_password;",
                new MySqlServerVersion(new Version(8, 0, 23)));
        }
    }
}
