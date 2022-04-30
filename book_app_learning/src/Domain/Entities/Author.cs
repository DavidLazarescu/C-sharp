using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The provided firstname is too short")]
        [MaxLength(40, ErrorMessage = "The provided firstname is too long")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "The provided firstname is too short")]
        [MaxLength(40, ErrorMessage = "The provided firstname is too long")]
        public string LastName { get; set; }

        public Book Book { get; set; }
    }
}