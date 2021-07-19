using System.Net;
using System.Threading.Tasks;

using interview.Domain.Models;
using interview.Domain.Services.Interfaces;
using interview.DTO;
using interview.Models;
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
        public async Task<IActionResult> AuthAsync([FromBody] AuthUserDTO authUserDto)
        {
            AuthServiceResult authServiceResult = await _authService.AuthAsync(authUserDto.Login, authUserDto.Password);

            return authServiceResult.Result switch
            {
                AuthServiceResults.Ok => Ok(new AuthResultApi { Token = authServiceResult.Token }),
                AuthServiceResults.InvalidCredentials => BadRequest("Invalid login or password."),
                _ => StatusCode((int)HttpStatusCode.NotImplemented),
            };
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AuthUserDTO authUserDto)
        {
            (AuthServiceResult authServiceResult, User user) = await _authService
                .RegisterAsync(authUserDto.Login, authUserDto.Password);

            var registerResult = new RegisterResultApi
            {
                Token = authServiceResult.Token,
                User = new UserApi
                {
                    Id = user.Id,
                    Login = user.Login,
                }
            };

            return authServiceResult.Result switch
            {
                AuthServiceResults.Ok => Ok(registerResult),
                AuthServiceResults.DuplicateLogin => BadRequest("Please, take another login."),
                _ => StatusCode((int)HttpStatusCode.NotImplemented),
            };
        }
    }
}