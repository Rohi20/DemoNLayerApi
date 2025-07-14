using DemoNLayerApi.Data.Context;
using DemoNLayerApi.Data.Exceptions;
using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDBContext _dbContext;

        public BookRepository(AppDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task AddBooks(Book book)
        {
            bool doesAuthorExists = await DoesAuthorExists(book.AuthorId);

            if (!doesAuthorExists)
            {
                throw new NotFoundException($"Author Id {book.AuthorId} doesn not exist");
            }

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBook(int id)
        {
            var book = await GetBooksById(id);
            if (book == null)
            {
                throw new NotFoundException("Book not found");
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _dbContext.Books.ToListAsync();
            return books;
        }

        public async Task<List<Book>> GetBooksByTitle(string title)
        {
            var books = await _dbContext.Books.Where(b => b.Title.Contains(title)).ToListAsync();
            return books;
        }

        public async Task<Book> GetBooksById(int id)
        {
            var book = await _dbContext.Books.Where(b => b.Id == id).FirstOrDefaultAsync();
            return book;
        }

        public async Task UpdateBook(Book book)
        {
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesAuthorExists(int id)
        {
            return await _dbContext.Books.AnyAsync(b =>  b.AuthorId == id);
        }
    }
}
