using System;
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
                    .Select(x => new AuthorResponse(x.Id, x.FullName, x.Email, x.Age, x.Rank))
                    .ToArray();
                return Ok(response);
            }
            catch (Exception e)
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

                var response = new AuthorResponse(author.Id, author.FullName, author.Email, author.Age, author.Rank);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
        [HttpPost("addAuthor")]
        public async Task<IActionResult> AddAsync([FromBody] CreateAuthorRequest createAuthorRequest)
        {
            var (fullName, email, age) = createAuthorRequest;
            try
            {
                if (string.IsNullOrEmpty(fullName))
                {
                    throw new Exception($"{nameof(fullName)} should be not empty");
                }

                if (string.IsNullOrEmpty(email) || (!email.Contains("@") && !email.Contains(".")))
                {
                    throw new Exception($"{nameof(email)} field should be an email address.");
                }

                if (age < 18)
                {
                    throw new Exception("Author age should be at least 18.");
                }

                string rank = string.Empty;

                if (fullName == "Bruce Lee")
                {
                    rank = "Advanced Author";
                }
                else if (fullName == "Uncle Bob")
                {
                    rank = "Intermediate Author";
                }
                else
                {
                    rank = "Unknown";
                }
                
                var author = new Author
                {
                    FullName = createAuthorRequest.FullName,
                    Email = createAuthorRequest.Email,
                    Age = age,
                    Rank = rank
                };

                await _authorService.AddAsync(author);

                var response = new AuthorResponse(author.Id, author.FullName, author.Email, author.Age, author.Rank);
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}