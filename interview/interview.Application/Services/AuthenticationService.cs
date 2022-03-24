using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using interview.Application.Data;
using interview.Application.Exceptions;
using interview.Application.Models;
using interview.Application.Options;
using interview.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace interview.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataContext _context;
        private readonly IOptions<JwtOptions> _jwtOptions;

        public AuthenticationService(DataContext dataContext, IOptions<JwtOptions> jwtOptions)
        {
            _context = dataContext;
            _jwtOptions = jwtOptions;
        }

        public async Task<string> AuthenticateAsync(string login, string password)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (user is null)
            {
                throw new AuthException($"User {login} has invalid credentials.");
            }

            string authToken = GenerateTokenForUser(user);

            return authToken;
        }

        private string GenerateTokenForUser(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Value.SecretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}