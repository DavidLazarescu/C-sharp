using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace backend_learning.Domain.Entities;

public class Job
{
    public int JobId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int YearlySalary { get; set; }

    [Required]
    public string Location { get; set; }

    [Required]
    public string Description { get; set; }

    // The "JsonIgnore" attribute is important for not creating a loop when mapping the "User" entity to a Json object.
    // The normal Json serializer recursively goes through the object it should serialize, e.g. for User it would go like:
    // UserId -> Name -> ... -> Job -> JobId -> Name -> ... -> User -> UserId -> ... -> Job -> JobId -> ...
    // Which is an infinite loop. By using the "JsonIgnore" attribute you are telling the serializer to not map it, so the loop is broken
    [JsonIgnore]
    public ICollection<User> User { get; set; }
}