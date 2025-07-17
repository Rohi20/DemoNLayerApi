using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Data.DataDTO
{
    public class BooksToCategory
    {
        public int Id { get; set; }
        public List<int> BookIds { get; set; }
    }
}
