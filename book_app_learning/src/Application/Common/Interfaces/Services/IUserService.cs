using Application.Common.Dtos;
using Application.Common.RequestParameters;


namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsersAsync(UserRequestParameter requestParameter);
        public Task<UserDto> GetUserByEmailAsync(string email);
        public Task RegisterUserAsync(RegisterDto registerDto);
    }
}