using System.ComponentModel.DataAnnotations;

namespace API_DemoBlazor.Models
{
    public class LoginForm
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
