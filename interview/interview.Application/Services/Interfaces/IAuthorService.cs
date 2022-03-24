using System.Threading.Tasks;
using interview.Application.Models;

namespace interview.Application.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<Author> GetByIdAsync(int id);
        
        Author[] Get();

        Task AddAsync(Author author);
    }
}