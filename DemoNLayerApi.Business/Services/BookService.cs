using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.IRepository;
using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        public BookService(IBookRepository repository)
        {
            _repository = repository;
            
        }
        public async Task AddBooks(Book book)
        {
            await _repository.AddBooks(book);
        }

        public async Task DeleteBook(int id)
        {
            await _repository.DeleteBook(id);
        }

        public async Task<List<Book>> GetAllBooks()
        {
            var books = await _repository.GetAllBooks();
            return books;
        }

        public async Task<List<Book>> GetBooksByTitle(string title)
        {
            var books = await _repository.GetBooksByTitle(title);
            return books;
        }

        public async Task UpdateBook(Book book)
        {
            await _repository.UpdateBook(book);
        }
        
        public async Task<Book> GetBookById(int id)
        {
            return await _repository.GetBookById(id);
        }

        public Task<List<BookPerAuthorDTO>> GetBooksPerAuthor()
        {
            return _repository.GetBooksPerAuthor();
        }

        public Task<List<BooksInRangeDTO>> GetBooksInRanges(decimal price)
        {
            return _repository.GetBooksInRange(price);
        }

        public Task<List<BooksByPriceDetails>> GetBooksByPriceDetails()
        {
            return _repository.GetBooksByPriceDetails();
        }
    }
}
