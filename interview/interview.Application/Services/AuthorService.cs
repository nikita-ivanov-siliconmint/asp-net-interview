using System.Linq;
using System.Threading.Tasks;
using interview.Application.Data;
using interview.Application.Models;
using interview.Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace interview.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;

        public AuthorService(DataContext context)
        {
            _context = context;
        }

        public Task<Author> GetByIdAsync(int id)
        {
            return _context.Authors.SingleOrDefaultAsync(x => x.Id == id);
        }

        public Author[] Get()
        {
            return _context.Authors.ToArray();
        }

        public Task AddAsync(Author author)
        {
            _context.Add(author);
            return _context.SaveChangesAsync();
        }
    }
}