using System.ComponentModel.DataAnnotations;

namespace Nullean.OnlineStore.OnlineStore.Models
{
    public class SignUpModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string PasswordRepeat { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
