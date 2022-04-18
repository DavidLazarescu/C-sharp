namespace backend_learning.Infrastructure.DTOs.User;

public class UserForUpdateDto
{
    public string Name { get; set; }

    public string Email { get; set; }

    public int Age { get; set; }

    public string SecretMessage { get; set; }

    public IEnumerable<string> Jobs { get; set; }
}