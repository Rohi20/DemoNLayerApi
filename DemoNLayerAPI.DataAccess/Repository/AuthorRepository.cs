using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDBContext _dbContext;

        public AuthorRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAuthorAsync(Author author)
        {
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(int id)
        {
            var author = _dbContext.Authors.Find(id);


            if (author == null)
            {
                throw new Exception("Author not found");
            }

            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Author>> GetAllAuthorsAsync()
        {
            return await _dbContext.Authors
                .Include(book => book.Books)
                .ThenInclude(category => category.Categories)
                .ToListAsync();
        }

        public async Task<Author> GetAuthorByIdAsync(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);

            if (author == null)
            {
                throw new Exception("Author not found");
            }

            return author;
        }

        public async Task UpdateAuthorAsync(int id, string name)
        {
            var author = _dbContext.Authors.Find(id);


            if (author == null)
            {
                throw new Exception("Author not found");
            }

            author.Name = name;

            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();
        }
    }
}
