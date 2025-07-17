using DemoNLayerApi.Data.DataDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.IRepository
{
    public interface IAggregationRepository
    {
        Task<List<BookPerAuthorDTO>> GetBooksPerAuthor();
        Task<List<BooksInRangeDTO>> GetBooksInRange(decimal price);
        /**
         * This function get the count of books, Maximum/minimum
         * price among the books
         * */
        Task<List<BooksByPriceDetails>> GetBooksByPriceDetails();
        Task<List<Top5Author>> GetTop5AuthorByBookCount();
    }
}
