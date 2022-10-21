using System.ComponentModel.DataAnnotations;

namespace UIUXLayer.Models
{
    public class UserRegisterRequest
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
