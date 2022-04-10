namespace backend_learning.Infrastructure.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public IEnumerable<string> Jobs { get; set; }
        
        public string Message { get; set; }
    }
}