using System.ComponentModel.DataAnnotations;

namespace backend_learning.Infrastructure.DTOs
{
    public class JobInputDto
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public int YearlySalary { get; set; }
        
        [Required]
        public string Location { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}