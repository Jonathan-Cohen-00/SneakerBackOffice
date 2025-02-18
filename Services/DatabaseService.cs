using System.Linq;
using SneakerBackOffice.Data;
using SneakerBackOffice.Models;

namespace SneakerBackOffice.Services
{
    public class DatabaseService
    {
        private readonly AppDbContext _context;

        public DatabaseService(AppDbContext context)
        {
            _context = context;
        }

        public bool AuthenticateUser(string email, string password)
        {
            return _context.Users.Any(u => u.Email == email && u.Password == password);
        }
    }
}
