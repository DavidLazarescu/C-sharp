using Application.Common.Dtos;
using Application.Common.RequestParameters;


namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsers(UserRequestParameter requestParameter);
    }
}