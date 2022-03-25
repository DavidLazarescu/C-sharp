using backend_learning.Entities;
using AutoMapper;


namespace backend_learning.DTOs
{
    // This is a Data Transfer Object (DTO), they exist to choose only certain data which should be sent to the user, this
    // saves bandwidth, because less data is sent over, and at the same time filters out properties you dont want to send
    public class UserDto
    {
        public string Name { get; set; }
        
        public string Email { get; set; }
        
        public int Age { get; set; }
        
        public IEnumerable<string> Jobs { get; set; }
    }
}