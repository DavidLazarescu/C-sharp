using Application.Dtos;
using Infrastructure.Interfaces.Repository;

namespace Application.Services
{
    public class AppUserService
    {
        private readonly IAppUserRepository _userRepository;

        
        public AppUserService(IAppUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<IEnumerable<AppUserDto>> GetAllUserDtos()
        {
            var users = await _userRepository.GetAllUsers();
            
            // return 
        }
    }
}