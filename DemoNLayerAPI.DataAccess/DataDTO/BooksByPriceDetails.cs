using DemoNLayerApi.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.DataDTO
{
    public class BookInfo
    {
        public string BookTitle { get; set; }
        public string Description { get; set; }
    }
    public class BooksByPriceDetails
    {
        public List<BookInfo> BookDetails { get; set; }
        public decimal Price { get; set; }
        public decimal PriceTotal { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int BooksCount { get; set; }

    }
}
