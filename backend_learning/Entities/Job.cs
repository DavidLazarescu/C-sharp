using System.Text.Json.Serialization;


namespace backend_learning.Entities
{
    public class Job
    {
        public int JobId { get; set; }

        public string Name { get; set; }
        
        public int YearlySalary { get; set; }
        
        public string Location { get; set; }
        
        public string Description { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public int UserId { get; set; }
    }
}