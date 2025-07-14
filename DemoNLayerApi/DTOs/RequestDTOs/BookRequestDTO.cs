using System.ComponentModel.DataAnnotations;

namespace DemoNLayerApi.DTOs.RequestDTOs
{
    public class BookRequestDTO
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public int AuthorId { get; set; }
    }
}
