using System.ComponentModel.DataAnnotations;

namespace DemoNLayerApi.Models.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public UserProfile Profile { get; set; }

    }
}
