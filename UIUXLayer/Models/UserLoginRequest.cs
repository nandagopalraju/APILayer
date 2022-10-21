using System.ComponentModel.DataAnnotations;

namespace UIUXLayer.Models
{
    public class UserLoginRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
