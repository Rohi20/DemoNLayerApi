using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Data.DataDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DemoNLayerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregationController : ControllerBase
    {
        private readonly IAggregationService _aggregationService;
        public AggregationController(IAggregationService aggregationService)
        {
            _aggregationService = aggregationService;
        }

        [HttpGet("get-books-per-author")]
        public async Task<ActionResult<List<BookPerAuthorDTO>>> GetBooksPerAuthor()
        {
            var books = await _aggregationService.GetBooksPerAuthor();
            return Ok(books);
        }

        [HttpPost("books-price-classification")]
        public async Task<ActionResult<List<BooksInRangeDTO>>> GetBooksInPriceRange(decimal price)
        {
            var books = await _aggregationService.GetBooksInRange(price);
            return Ok(books);
        }

        [HttpGet("get-books-by-price-details")]
        public async Task<ActionResult<List<BooksByPriceDetails>>> GetBooksByPriceDetails()
        {
            var bookDetails = await _aggregationService.GetBooksByPriceDetails();
            return Ok(bookDetails);
        }

        [HttpGet("get-top-five-authors-by-books")]
        public async Task<ActionResult<List<Top5Author>>> GetTop5AuthorByBookCount()
        {
            var authors = await _aggregationService.GetTop5AuthorByBookCount();
            return Ok(authors);
        }

    }
}
