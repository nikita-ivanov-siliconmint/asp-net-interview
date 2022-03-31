using System.IO;
using System.Threading.Tasks;
using interview.Application.Services.Interfaces;
using interview.Contracts.Requests;
using interview.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace interview.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserRequest loginRequest)
        {
            try
            {
                var token = await _authenticationService.AuthenticateAsync(loginRequest.Login, loginRequest.Password);
                var response = new AuthSuccessResponse(token);
                return Ok(response);
            }
            catch (InvalidDataException e)
            {
                var response = new ErrorResponse(e.Message);
                return BadRequest(response);
            }
        }
    }
}