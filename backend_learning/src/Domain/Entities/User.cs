using System.ComponentModel.DataAnnotations;


namespace backend_learning.Domain.Entities;

public class User
{
    public int UserId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public DateTime TimeOfCreation { get; set; }

    public int Age { get; set; }

    [Required]
    public byte[] Password { get; set; }

    [Required]
    public byte[] PasswordSeed { get; set; }

    public string SecretMessage { get; set; }

    public IEnumerable<Job> Jobs { get; set; }
}