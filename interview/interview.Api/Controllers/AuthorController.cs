using System.IO;
using System.Linq;
using System.Threading.Tasks;
using interview.Application.Models;
using interview.Application.Services.Interfaces;
using interview.Contracts.Requests;
using interview.Contracts.Responses;
using Microsoft.AspNetCore.Mvc;

namespace interview.Controllers
{
    [Route("api")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("getAuthors")]
        public IActionResult GetCollectionAsync()
        {
            try
            {
                Author[] authors = _authorService.Get();
                AuthorResponse[] response = authors
                    .Select(x => new AuthorResponse
                    {
                        Id = x.Id,
                        FullName = x.FullName,
                        Age = x.Age,
                        Email = x.Email
                    })
                    .ToArray();
                return Ok(response);
            }
            catch (InvalidDataException e)
            {
                var response = new ErrorResponse(e.Message);
                return BadRequest(response);
            }
        }

        [HttpGet("getAuthorById/{ib}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                Author author = await _authorService.GetByIdAsync(id);

                if (author is null)
                {
                    var errorResponse = new ErrorResponse($"Author with id {id} not found");
                    return NotFound(errorResponse);
                }

                var response = new AuthorResponse
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    Age = author.Age,
                    Email = author.Email
                };
                return Ok(response);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("addAuthor")]
        public async Task<IActionResult> AddAsync([FromBody] CreateAuthorRequest createAuthorRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(createAuthorRequest.FullName))
                {
                    throw new InvalidDataException($"{nameof(createAuthorRequest.FullName)} should be not empty");
                }

                if (string.IsNullOrEmpty(createAuthorRequest.Email) || (!createAuthorRequest.Email.Contains("@") && !createAuthorRequest.Email.Contains(".")))
                {
                    throw new InvalidDataException($"{nameof(createAuthorRequest.Email)} field should be an email address.");
                }

                if (createAuthorRequest.Age < 18)
                {
                    throw new InvalidDataException("Author age should be at least 18.");
                }

                var author = new Author
                {
                    FullName = createAuthorRequest.FullName,
                    Email = createAuthorRequest.Email,
                    Age = createAuthorRequest.Age
                };

                await _authorService.AddAsync(author);

                var response = new AuthorResponse
                {
                    Id = author.Id,
                    FullName = author.FullName,
                    Age = author.Age,
                    Email = author.Email
                };
                return Ok(response);
            }
            catch (InvalidDataException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}