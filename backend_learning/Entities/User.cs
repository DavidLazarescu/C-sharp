using System.ComponentModel.DataAnnotations;

namespace backend_learning.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]  // This is a Data annotation, it does some validation on the data, here it says that the Name cant be null
        public string Name { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public DateTime TimeOfCreation { get; set; }
        
        [Required]
        public int Age { get; set; }

        [Required]
        public byte[] Password { get; set; }

        [Required]
        public byte[] PasswordSeed { get ; set; }
        
        public string SecretMessage { get; set; }

        public IEnumerable<Job> Jobs { get; set; }
    }
}