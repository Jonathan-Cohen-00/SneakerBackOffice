using System.Linq;
using SneakerBackOffice.Data;
using SneakerBackOffice.Models;
using BCrypt.Net;

namespace SneakerBackOffice.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public bool ValidateUser(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            return user != null && BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
