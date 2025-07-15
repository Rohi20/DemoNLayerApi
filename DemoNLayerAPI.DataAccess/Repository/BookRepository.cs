using DemoNLayerApi.Data.Context;
using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.Exceptions;
using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

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
            var book = await GetBookById(id) ?? throw new NotFoundException("Book not found");
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

        public async Task<Book> GetBookById(int id)
        {
            var book = await _dbContext.Books.Where(b => b.Id == id).Include(b => b.Author).FirstOrDefaultAsync();
            return book;
        }

        public async Task UpdateBook(Book book)
        {
            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> DoesAuthorExists(int id)
        {
            return await _dbContext.Books.AnyAsync(b => b.AuthorId == id);
        }

        public async Task<List<BookPerAuthorDTO>> GetBooksPerAuthor()
        {
            var authorStats = await _dbContext.Books.
                 GroupBy(b => b.Author.Name).
                 Select(c => new BookPerAuthorDTO
                 {
                     AuthorName = c.Key,
                     BookCount = c.Count(),
                     TotalPrice = c.Sum(b => b.Price),
                     AveragePrice = c.Average(b => b.Price)
                 }).ToListAsync();

            return authorStats;

        }

        public Task<List<BooksInRangeDTO>> GetBooksInRange(decimal p)
        {
            var books = _dbContext.Books
                .GroupBy(b => b.Price < p ? "Low" : "High")
                .Select(t => new BooksInRangeDTO
                {
                    PriceCategory = t.Key,
                    BookCount = t.Count(),
                }).ToListAsync();

            return books;
        }
        public async Task<List<BooksByPriceDetails>> GetBooksByPriceDetails()
        {

            /*var books = await _dbContext.Books.ToListAsync();

           var booksGroup = books
                 .GroupBy(b => b.Price)
                 .Select(b => new BooksByPriceDetails
                 {
                     Price = b.Key,
                     MinPrice = b.Min(b => b.Price),
                     MaxPrice = b.Max(b => b.Price),
                     BooksCount = b.Count(),
                     BookDetails = b.Select(b => new BookInfo
                     {
                         BookTitle = b.Title,
                         Description = b.Description,
                     }).ToList()
                 }).ToList();*/


            var booksAggr = await _dbContext.Books
                 .GroupBy(b => b.Price)
                 .Select(b => new BooksByPriceDetails
                 {
                     Price = b.Key,
                     MinPrice = b.Min(b => b.Price),
                     MaxPrice = b.Max(b => b.Price),
                     BooksCount = b.Count()
                 }).ToListAsync();

            foreach(var aggr in booksAggr)
            {
              aggr.BookDetails =  await _dbContext.Books.Where(t => t.Price == aggr.Price).Select(b => new BookInfo
                {
                    BookTitle = b.Title,
                    Description = b.Description,
                    
                }).ToListAsync();
            }

            return booksAggr;
        }
    }
}
