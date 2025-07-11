using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace DemoNLayerApi.DTOs.RequestDTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BookDTO> Books { get; set; }
    }

    public class CreateAuthorDTO
    {
        public int Id { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public string BookTitle { get; set; }
        public string Description { get; set; }
        [Required]
        public string BookCategory { get; set; }
    }
}
