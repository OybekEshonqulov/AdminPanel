using System.ComponentModel.DataAnnotations;

namespace AdminPanel1.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Eslab qolish")]
        public bool RememberMe { get; set; }
    }
}
