using System.ComponentModel.DataAnnotations;

namespace DemoNLayerApi.Models.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
