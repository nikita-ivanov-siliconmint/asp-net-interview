using System.Threading.Tasks;

using interview.Domain.Models;

namespace interview.Domain.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthServiceResult> AuthAsync(string login, string password);

        public Task<(AuthServiceResult, User)> RegisterAsync(string login, string password);
    }
}