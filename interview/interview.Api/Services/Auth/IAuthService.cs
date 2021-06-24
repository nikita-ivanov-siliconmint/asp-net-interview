using System.Threading.Tasks;

using interview.Domain.Models;
using interview.Models;

namespace interview.Services.Auth
{
    public interface IAuthService
    {
        public Task<AuthServiceResult> AuthAsync(string login, string password);

        public Task<(AuthServiceResult, User)> RegisterAsync(string login, string password);
    }
}