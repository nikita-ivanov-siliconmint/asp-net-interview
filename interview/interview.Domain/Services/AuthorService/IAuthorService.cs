using System.Threading.Tasks;

using interview.Domain.Models;

namespace interview.Domain.Services.AuthorService
{
    public interface IAuthorService
    {
        public Task<Author> GetFullAuthorByIdAsync(int id);

        public Task<Author> AddAsync(Author author);
    }
}