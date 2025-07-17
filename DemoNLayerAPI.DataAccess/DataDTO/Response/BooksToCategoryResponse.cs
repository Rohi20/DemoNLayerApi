using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.DataDTO.Response
{
    public class BooksLinked
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
    }
    public class BooksToCategoryResponse
    {
        public string CategoryName {  get; set; }
        public List<BooksLinked> BooksLinked {  get; set; }
    }
}
