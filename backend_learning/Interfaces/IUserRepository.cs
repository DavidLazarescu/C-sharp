using backend_learning.DTOs;
using backend_learning.Entities;


namespace backend_learning.Interfaces
{
    // The repository pattern implements a layer of abstraction between the classes which want to access the database and
    // EF core. You are able to write cleaner and more understandable code, by not writing the LINQ queries yourself, but
    // calling methods which do what you want 
    public interface IUserRepository
    {
        public Task<bool> SaveChanges();
        public Task AddUser(User user);
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserById(int id);
        public Task<UserDto> GetUserDtoByEmail(string email);
        public Task<UserDto> GetUserDtoById(int id);
        public Task<bool> ContainsUserWithEmail(string email);
        public Task<IEnumerable<UserDto>> GetAllUserDtos();
    }
}