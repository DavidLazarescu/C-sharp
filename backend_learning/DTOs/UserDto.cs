using backend_learning.Entities;
using AutoMapper;


namespace backend_learning.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public int Age { get; set; }
        
        public Job Job { get; set; }
    }
}