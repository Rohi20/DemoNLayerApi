using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.DataDTO
{
    public class BookPerAuthorDTO
    {

        public string AuthorName { get; set; }
        public int BookCount { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal AveragePrice { get; set; }
    }
}
