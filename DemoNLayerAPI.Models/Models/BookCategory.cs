using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNLayerApi.Models.Models
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public Category Category { get; set; }

        public int CategoryId { get; set; }
        public Book Book { get; set; }
    }
}
