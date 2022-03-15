using backend_learning.Data;
using backend_learning.DTOs;
using backend_learning.Entities;
using backend_learning.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend_learning.Controllers
{
    public class AccountController : BaseController
    {
        private readonly DataContext _context;
        private readonly IUserRepository _userRepository;

        public AccountController(DataContext context, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _context = context;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(RegisterDto registerDTO)
        {
            if (await _userRepository.ContainsUserWithEmail(registerDTO.Email))
                return BadRequest("A user with this email already exists");

            User user = new User
            {
                Name = registerDTO.Name,
                Age = registerDTO.Age,
                Email = registerDTO.Email,
                Job = registerDTO.Job,
                SecretMessage = registerDTO.Message,
                Password = registerDTO.Password,
                TimeOfCreation = DateTime.UtcNow
            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<ActionResult<User>> Login(LoginDto loginDto)
        {
            if(await _context.Users.AnyAsync(user => user.Email == loginDto.Email && user.Password == loginDto.Password))
                return BadRequest("The provided email or password is wrong");

            var user = await _context.Users
                .Where(user => user.Email == loginDto.Email && user.Password == loginDto.Password)
                .FirstAsync();

            return Ok(user);
        }
    }
}