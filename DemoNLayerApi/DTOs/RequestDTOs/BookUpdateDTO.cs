using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace DemoNLayerApi.DTOs.RequestDTOs
{
    public class BookUpdateDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }

    }
}
