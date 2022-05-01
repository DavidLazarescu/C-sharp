using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos
{
    public class LoginDto
    {
        [Required]
        [MinLength(6, ErrorMessage = "The provided email is too short")]
        [MaxLength(50, ErrorMessage = "The provided email is too long")]
        public string Email { get; set; }
        
        [Required]
        [MinLength(6, ErrorMessage = "The provided password is too short")]
        [MaxLength(60, ErrorMessage = "The provided password is too long")]
        public string Password { get; set; }
    }
}