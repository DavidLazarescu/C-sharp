using backend_learning.Infrastructure.DTOs.User;
using backend_learning.Domain.Entities;
using Infrastructure.RequestObjects;


namespace backend_learning.Infrastructure.Interfaces;

// The repository pattern implements a layer of abstraction between the classes which want to access the database and
// EF core. You are able to write cleaner and more understandable code, by not writing the LINQ queries yourself, but
// calling methods which do what you want 
public interface IUserRepository
{
    public Task<bool> SaveChangesAsync();
    public Task AddUserAsync(User user);
    public Task<User> GetUserByEmailAsync(string email, bool trackChanges);
    public Task<User> GetUserByIdAsync(int id, bool trackChanges);
    public Task<UserOutputDto> GetUserDtoByEmailAsync(string email, bool trackChanges);
    public Task<UserOutputDto> GetUserDtoByIdAsync(int id, bool trackChanges);
    public Task<IEnumerable<Job>> GetJobsByEmail(string email, JobRequestObject jobRequestObject);
    public Task<bool> UserAlreadyExistsAsync(string email);
    public Task<IEnumerable<UserOutputDto>> GetAllUserDtosAsync(bool trackChanges);
    public void Delete(User user);
}