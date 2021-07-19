using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using interview.Domain.Models;
using interview.Domain.Options;
using interview.Domain.Services.Interfaces;

namespace interview.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IOptions<JwtOptions> _jwtOptions;


        public AuthService(DataContext dataContext, IOptions<JwtOptions> jwtOptions)
        {
            _context = dataContext;
            _jwtOptions = jwtOptions;
        }


        public async Task<AuthServiceResult> AuthAsync(string login, string password)
        {
            User user = await _context.Users
                .FirstOrDefaultAsync(x => x.Login == login && x.Password == password);

            if (user == null)
            {
                return new AuthServiceResult
                {
                    Token = null,
                    Result = AuthServiceResults.InvalidCredentials,
                };
            }

            string token = GenerateTokenForUser(user);

            return new AuthServiceResult
            {
                Token = token,
                Result = AuthServiceResults.Ok,
            };
        }

        public async Task<(AuthServiceResult, User)> RegisterAsync(string login, string password)
        {
            User userLoginCheck = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);

            if (userLoginCheck != null)
            {
                var duplicateResult = new AuthServiceResult
                {
                    Token = null,
                    Result = AuthServiceResults.DuplicateLogin,
                };

                return (duplicateResult, null);
            }

            var user = new User
            {
                Login = login,
                Password = password,
                Role = UserRole.Default,
            };
            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            string token = GenerateTokenForUser(user);

            var authServiceResult = new AuthServiceResult
            {
                Token = token,
                Result = AuthServiceResults.Ok,
            };

            return (authServiceResult, user);
        }

        private string GenerateTokenForUser(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Value.SecretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.Now.Add(_jwtOptions.Value.Expires),
                Issuer = _jwtOptions.Value.Issuer,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}