using backend_learning.Entities;
using AutoMapper;


namespace backend_learning.DTOs
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