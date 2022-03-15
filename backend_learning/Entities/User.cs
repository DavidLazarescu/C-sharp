using System.ComponentModel.DataAnnotations;

namespace backend_learning.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime TimeOfCreation { get; set; }
        
        public int Age { get; set; }

        public Job Job { get; set; }

        public string Password { get; set; }
        
        public string SecretMessage { get; set; }
    }
}