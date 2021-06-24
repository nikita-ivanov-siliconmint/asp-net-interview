using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using interview.DTO;
using interview.Models;
using interview.Domain.Models;
using interview.Domain.Services.AuthorService;

namespace interview.Controllers
{
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;


        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }


        [HttpGet]
        public async Task<IActionResult> GetCollectionAsync()
        {
            return Ok();
        }

        [HttpGet("{ib}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Author authorEntity = await _authorService.GetFullAuthorByIdAsync(id);

            var author = new AuthorApi
            {
                Id = authorEntity.Id,
                Age = authorEntity.Age,
                FullName = authorEntity.FullName,
            };

            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] CreateAuthorDTO authorDto)
        {
            var author = new Author
            {
                FullName = authorDto.FullName,
                Age = authorDto.Age,
            };

            await _authorService.AddAsync(author);

            var authorToReturn = new AuthorApi
            {
                Id = author.Id,
                Age = author.Age,
                FullName = author.FullName,
            };

            return Ok(authorToReturn);
        }
    }
}