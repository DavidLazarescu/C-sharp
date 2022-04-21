using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public DateTime PublishingDate { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Pages { get; set; }

        [MaxLength(40, ErrorMessage = "The book format name is too long")]
        public string Format { get; set; }

        public IEnumerable<Author> Authors { get; set; }
    }
}