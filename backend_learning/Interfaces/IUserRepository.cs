using backend_learning.DTOs;
using backend_learning.Entities;


namespace backend_learning.Interfaces
{
    public interface IUserRepository
    {
        public Task<bool> SaveChanges();
        public Task<User> GetUserByEmail(string email);
        public Task<User> GetUserById(int id);
        public Task<UserDto> GetUserDtoByEmail(string email);
        public Task<UserDto> GetUserDtoById(int id);
        public Task<bool> ContainsUserWithEmail(string email);
        public Task<IEnumerable<UserDto>> GetAllUserDtos();
    }
}