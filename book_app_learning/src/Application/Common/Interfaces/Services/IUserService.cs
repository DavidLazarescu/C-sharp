using Application.Common.Dtos;
using Application.Common.RequestParameters;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Application.Interfaces.Services
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsersAsync(UserRequestParameter requestParameter);
        public Task<UserDto> GetUserByEmailAsync(string email);
        public Task RegisterUserAsync(RegisterDto registerDto);
        public Task PatchUserAsync(string email, JsonPatchDocument<UserUpdateDto> patchDoc, ControllerBase controllerBase);
    }
}