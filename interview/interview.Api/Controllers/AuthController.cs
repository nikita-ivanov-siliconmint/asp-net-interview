using System.Net;
using System.Threading.Tasks;

using interview.Domain.Models;
using interview.DTO;
using interview.Models;
using interview.Services.Auth;

using Microsoft.AspNetCore.Mvc;

namespace interview.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;


        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpGet("auth")]
        public async Task<IActionResult> AuthAsync([FromBody] AuthUser authUser)
        {
            AuthServiceResult authServiceResult = await _authService.AuthAsync(authUser.Login, authUser.Password);

            return authServiceResult.Result switch
            {
                AuthServiceResults.Ok => Ok(new AuthResultApi { Token = authServiceResult.Token }),
                AuthServiceResults.InvalidCredentials => BadRequest("Invalid login or password."),
                _ => StatusCode((int)HttpStatusCode.NotImplemented),
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AuthUser authUser)
        {
            (AuthServiceResult authServiceResult, User user) = await _authService
                .RegisterAsync(authUser.Login, authUser.Password);

            // TODO return RegisterResultApi when OK

            return authServiceResult.Result switch
            {
                AuthServiceResults.Ok => Ok(),
                AuthServiceResults.DuplicateLogin => BadRequest("Please, take another login."),
                _ => StatusCode((int)HttpStatusCode.NotImplemented),
            };
        }
    }
}