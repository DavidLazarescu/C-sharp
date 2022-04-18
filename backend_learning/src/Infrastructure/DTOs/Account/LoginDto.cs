using System.ComponentModel.DataAnnotations;


namespace backend_learning.Infrastructure.DTOs.Account;

public class LoginDto
{
    [Required(ErrorMessage = "An email is required!")]
    [MinLength(6, ErrorMessage = "The provided email is too short")]
    [MaxLength(50, ErrorMessage = "The provided email is too long")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A password is required")]
    [MinLength(6, ErrorMessage = "The provided password is too short")]
    [MaxLength(100, ErrorMessage = "The provided password is too long")]
    public string Password { get; set; }
}