using System.ComponentModel.DataAnnotations;


namespace backend_learning.Infrastructure.DTOs.Account;

public class RegisterDto
{
    [Required(ErrorMessage = "An email is required!")]
    [MinLength(6, ErrorMessage = "The provided email is too short")]
    [MaxLength(50, ErrorMessage = "The provided email is too long")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A name is required")]
    [MinLength(2, ErrorMessage = "The provided name is too short")]
    [MaxLength(40, ErrorMessage = "The provided name is too long")]
    public string Name { get; set; }

    [Required(ErrorMessage = "A password is required")]
    [MinLength(6, ErrorMessage = "The provided password is too short")]
    [MaxLength(100, ErrorMessage = "The provided password is too long")]
    public string Password { get; set; }

    [Range(12, 150, ErrorMessage = "The provided age needs to be in the range of 12-150")]
    public int Age { get; set; }

    public IEnumerable<string> Jobs { get; set; }

    [MaxLength(400, ErrorMessage = "The provided message is too long")]
    public string Message { get; set; }
}