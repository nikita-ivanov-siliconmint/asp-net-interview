using System.Threading.Tasks;
using interview.Application.Models;

namespace interview.Application.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<string> AuthenticateAsync(string login, string password);
    }
}