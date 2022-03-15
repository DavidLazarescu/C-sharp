using backend_learning.Data;
using backend_learning.DTOs;
using backend_learning.Entities;
using backend_learning.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend_learning.Controllers
{
    [Route("api/users")]
    public class UserController : BaseController
    {
        private readonly IUserRepository _userRepository;


        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers() => Ok(await _userRepository.GetAllUserDtos());

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUser(string email) => Ok(await _userRepository.GetUserDtoByEmail(email));
    }
}