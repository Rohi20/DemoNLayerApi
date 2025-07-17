using DemoNLayerApi.Data.DataDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.IServices
{
    public interface IAggregationService
    {
        Task<List<BookPerAuthorDTO>> GetBooksPerAuthor();
        Task<List<BooksInRangeDTO>> GetBooksInRange(decimal price);
        Task<List<BooksByPriceDetails>> GetBooksByPriceDetails();
        Task<List<Top5Author>> GetTop5AuthorByBookCount();
    }
}
