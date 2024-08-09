using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using OrdenesInversionAPI.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OrdenesInversionAPI.Services
{
    public class UserService : IUserService
    {
        private readonly OrdenesInversionContext _context;

        public UserService(OrdenesInversionContext context)
        {
            _context = context;
        }

        public async Task<Usuario> Authenticate(string username, string password, string scheme)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "Usuario")
            };
            var identity = new ClaimsIdentity(claims, scheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, scheme);

            return user;
        }
    }
}
