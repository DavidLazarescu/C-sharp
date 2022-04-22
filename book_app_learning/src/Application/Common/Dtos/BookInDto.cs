using System.ComponentModel.DataAnnotations;

namespace Application.Common.Dtos
{
    public class BookInDto
    {
        [Required]
        [MinLength(4, ErrorMessage = "The provided book name is too short")]
        [MaxLength(200, ErrorMessage = "The provided book name is too long")]
        public string Name { get; set; }

        [Required]
        public DateTime PublishingDate { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Pages { get; set; }

        [MaxLength(40, ErrorMessage = "The provided book format name is too long")]
        public string Format { get; set; }

        public IEnumerable<AuthorInDto> Authors { get; set; }
    }
}