using backend_learning.Infrastructure.DTOs;
using backend_learning.Domain.Entities;

namespace backend_learning.Infrastructure.Interfaces
{
    // The repository pattern implements a layer of abstraction between the classes which want to access the database and
    // EF core. You are able to write cleaner and more understandable code, by not writing the LINQ queries yourself, but
    // calling methods which do what you want 
    public interface IUserRepository
    {
        public Task<bool> SaveChanges();
        public Task AddUser(User user);
        public Task<User> GetUserByEmail(string email, bool trackChanges);
        public Task<User> GetUserById(int id, bool trackChanges);
        public Task<UserDto> GetUserDtoByEmail(string email, bool trackChanges);
        public Task<UserDto> GetUserDtoById(int id, bool trackChanges);
        public Task<bool> UserAlreadyExists(string email);
        public Task<IEnumerable<UserDto>> GetAllUserDtos(bool trackChanges);
    }
}