﻿using System;
using System.Threading.Tasks;

using interview.Domain.Models;

namespace interview.Domain.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly DataContext _context;


        public AuthorService(DataContext context)
        {
            _context = context;
        }


        public async Task<Author> GetFullAuthorByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Author> AddAsync(Author author)
        {
            _context.Add(author);

            await _context.SaveChangesAsync();

            return author;
        }
    }
}