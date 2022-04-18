namespace backend_learning.Infrastructure.DTOs.Job;

public class JobOutputDto
{
    public int JobId { get; set; }

    public string Name { get; set; }

    public int YearlySalary { get; set; }

    public string Location { get; set; }

    public string Description { get; set; }
}