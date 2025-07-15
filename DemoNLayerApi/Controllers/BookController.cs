using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.Exceptions;
using DemoNLayerApi.DTOs.RequestDTOs;
using DemoNLayerApi.Models.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoNLayerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("get-books")]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpPost("add-book")]
        public async Task<ActionResult> AddBooks([FromBody] BookRequestDTO bookRequest)
        {
            var book = new Book{
                Title = bookRequest.Title,
                Description = !String.IsNullOrWhiteSpace(bookRequest.Description) 
                ? bookRequest.Description : string.Empty,
                AuthorId = bookRequest.AuthorId,
            };

            await _bookService.AddBooks(book);
            return Ok("Book added successfully");

        }

        [HttpGet("get-books-by-title")]
        public async Task<ActionResult<List<Book>>> GetBooksByTitle(string title)
        {
            var books = await _bookService.GetBooksByTitle(title);
            return Ok(books);
        }

        [HttpPut("update-book")]
        public async Task<ActionResult> UpdateBook([FromBody] BookUpdateDTO bookRequest)
        {
            var existingBook = await _bookService.GetBookById(bookRequest.Id);

            if (bookRequest.AuthorId.HasValue)
            {                
                existingBook.AuthorId = bookRequest.AuthorId.Value;
            }

            if (!String.IsNullOrEmpty(bookRequest.Title)){
                existingBook.Title = bookRequest.Title;
            }

            if (!String.IsNullOrEmpty(bookRequest.Description))
            {
                existingBook.Description = bookRequest.Description;
            }

            if (bookRequest.Price.HasValue)
            {
                existingBook.Price = bookRequest.Price.Value;
            }
          

            await _bookService.UpdateBook(existingBook);
            return Ok("Book updated successfully");
        }

        [HttpDelete("delete-book")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _bookService.DeleteBook(id);
            return Ok("Book deleted successfully");
        }

        [HttpGet("get-books-per-author")]
        public async Task<ActionResult<List<BookPerAuthorDTO>>> GetBooksPerAuthor()
        {
            var books = await _bookService.GetBooksPerAuthor();
            return Ok(books);
        }

        [HttpPost("get-books-by-price")]
        public async Task<ActionResult<List<BooksInRangeDTO>>> GetBooksInPriceRange(decimal price)
        {
            var books = await _bookService.GetBooksInRanges(price);
            return Ok(books);
        }

        [HttpGet("get-books-by-price-details")]
        public async Task<ActionResult<List<BooksByPriceDetails>>> GetBooksByPriceDetails()
        {
            var bookDetails = await _bookService.GetBooksByPriceDetails();
            return Ok(bookDetails);
        }
    }
}
