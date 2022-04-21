using Application.Dtos;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetAllUserDtos();
    }
}