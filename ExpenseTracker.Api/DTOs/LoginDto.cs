using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Api.DTOs
{
    public class LoginDto
    {
        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
