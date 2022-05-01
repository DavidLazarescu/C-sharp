using Application.Common.Dtos;

namespace Application.Common.Interfaces.Services
{
    public interface IAuthenticationService
    {
        public Task RegisterUser(RegisterDto registerDto);
        public Task<string> AuthenticateUser(LoginDto loginDto);
    }
}