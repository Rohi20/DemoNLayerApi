using DemoNLayerApi.Business.IServices;
using DemoNLayerApi.Data.DataDTO;
using DemoNLayerApi.Data.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Business.Services
{
    public class AggregationService : IAggregationService
    {
        private readonly IAggregationRepository _aggregationRepository;

        public AggregationService(IAggregationRepository aggregationRepository)
        {
            _aggregationRepository = aggregationRepository;
        }

        public Task<List<BooksByPriceDetails>> GetBooksByPriceDetails()
        {
            return _aggregationRepository.GetBooksByPriceDetails();
        }

        public Task<List<BooksInRangeDTO>> GetBooksInRange(decimal price)
        {
            return _aggregationRepository.GetBooksInRange(price);
        }

        public Task<List<BookPerAuthorDTO>> GetBooksPerAuthor()
        {
            return _aggregationRepository.GetBooksPerAuthor();
        }

        public Task<List<Top5Author>> GetTop5AuthorByBookCount()
        {
            return _aggregationRepository.GetTop5AuthorByBookCount();

        }
    }
}
