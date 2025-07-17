using DemoNLayerApi.Data.Context;
using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.Repository
{
    public class AggregationRepository: IAggregationRepository
    {
        private readonly AppDBContext _dbContext;

        public AggregationRepository(AppDBContext dbContext)
        {
            _dbContext = dbContext;
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

            foreach (var aggr in booksAggr)
            {
                aggr.BookDetails = await _dbContext.Books
                    .Where(t =>  t.Price == aggr.Price)
                    .Select(b => new BookInfo
                {
                    BookTitle = b.Title,
                    Description = b.Description,

                }).ToListAsync();
            }

            return booksAggr;
        }

        public Task<List<Top5Author>> GetTop5AuthorByBookCount()
        {
            var authors = _dbContext.Authors
                .Select(a => new Top5Author
                {
                    AuthorName = a.Name,
                    BookCount = a.Books.Count(),
                })
                .OrderByDescending(a => a.BookCount)
                .Take(5)
                .ToListAsync();

            return authors;
        }
    }
}
