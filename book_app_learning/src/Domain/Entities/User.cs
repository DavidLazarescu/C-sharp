using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        public int UserId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The provided firstname is too short")]
        [MaxLength(40, ErrorMessage = "The provided firstname is too long")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The provided firstname is too short")]
        [MaxLength(50, ErrorMessage = "The provided firstname is too long")]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Range(0, 120, ErrorMessage = "The provided age needs to be between 0 and 120")]
        public int Age { get; set; }

        [Required]
        public byte[] Password { get; set; }

        [Required]
        public byte[] PasswordSeed { get; set; }

        [Required]
        public DateTime AccountCreation { get; set; }

        public IEnumerable<Book> Books { get; set; }
    }
}