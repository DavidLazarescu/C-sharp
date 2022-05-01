using Application.Common.Dtos;

namespace Application.Common.Interfaces.Managers
{
    public interface IAuthenticationManager
    {
         Task<bool> ValidateUser(LoginDto loginDto);
         Task<string> CreateToken();
    }
}