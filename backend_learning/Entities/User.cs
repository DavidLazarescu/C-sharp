using System.ComponentModel.DataAnnotations;

namespace backend_learning.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]  // This is a Data annotation, it does some validation on the data, here it says that the Name cant be null
        public string Name { get; set; }
        
        public string Email { get; set; }

        public DateTime TimeOfCreation { get; set; }
        
        public int Age { get; set; }

        public Job Job { get; set; }

        public byte[] Password { get; set; }

        public byte[] PasswordSeed { get ; set; }
        
        public string SecretMessage { get; set; }
    }
}