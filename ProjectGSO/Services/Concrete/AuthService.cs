using System.ComponentModel.DataAnnotations;
using ProjectGSO.DbContext;
using ProjectGSO.Models;
using ProjectGSO.Services.Abstrack;
using static System.Net.WebRequestMethods;

namespace ProjectGSO.Services.Concrete
{
    public class AuthService: IAuthService
    {
        private readonly BlogDbContext _context;

        public AuthService(BlogDbContext context)
        {
            _context = context;
        }


        public Users Login(string mail, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == mail);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password,user.Password))
            {
                return null;
            }

            return user;
        }

        public bool Register(string firstName, string lastName, string mail, string password)
        {
            if (_context.Users.Any(u=>u.Email == mail))
            {
                return false;
            }
            var user = new Users
            {
                FirstName = firstName,
                LastName = lastName,
                Email = mail,
                AvatarUrl = "https://static.thenounproject.com/png/354384-200.png",
                Role = "User",
                Username = "Kullanıcı",
                Password = BCrypt.Net.BCrypt.HashPassword(password)
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }
    }
}
