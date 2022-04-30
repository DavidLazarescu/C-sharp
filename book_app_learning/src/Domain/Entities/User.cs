using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IdentityUser
    {
        public string UserId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The provided firstname is too short")]
        [MaxLength(40, ErrorMessage = "The provided firstname is too long")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The provided firstname is too short")]
        [MaxLength(50, ErrorMessage = "The provided firstname is too long")]
        public string LastName { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "The provided email is too short")]
        [MaxLength(50, ErrorMessage = "The provided email is too long")]
        public override string Email { get; set; }

        [Required]
        [Range(0, 120, ErrorMessage = "The provided age needs to be between 0 and 120")]
        public int Age { get; set; }

        [Required]
        public DateTime AccountCreation { get; set; }

        
        public IEnumerable<Book> Books { get; set; }
    }
}