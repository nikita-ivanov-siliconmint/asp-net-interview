using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace interview.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCollection()
        {
            return Ok();
        }

        [HttpGet("{ib}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            return Ok();
        }
    }
}