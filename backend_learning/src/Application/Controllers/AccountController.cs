using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using backend_learning.Infrastructure.Interfaces;
using backend_learning.Infrastructure.DTOs;
using backend_learning.Domain.Entities;

namespace backend_learning.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IMapper _mapper;


        public AccountController(IUserRepository userRepository, IJobRepository jobRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _jobRepository = jobRepository;
            _mapper = mapper;
        }


        [HttpPost("register")]  // Specifies that this endpoint is reachable by a POST request to the endpoint "x/api/register"
        public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto registerDto)
        {
            // Checks if the email already exists
            if (await _userRepository.UserAlreadyExists(registerDto.Email))
                return BadRequest("A user with this email already exists");

            // hash the password for input password get the password and its hashing key back
            var hashedPasswordAndKey = GetHashAndKey(registerDto.Password);

            // Create a user with all the data gathered by the registerDto and the hashing of the password
            User user = new User
            {
                Name = registerDto.Name,
                Age = registerDto.Age,
                Email = registerDto.Email,
                SecretMessage = registerDto.Message,
                Password = hashedPasswordAndKey.Item1,
                PasswordSeed = hashedPasswordAndKey.Item2,
                TimeOfCreation = DateTime.UtcNow
            };

            // Find the jobs with the requested names frome the database (Jobs table) and add it to a user
            List<Job> jobs = new List<Job>();
            foreach (string jobName in registerDto.Jobs)
            {
                var job = await _jobRepository.GetJobWithName(jobName, trackChanges: true);
                
                if(job != null)
                    jobs.Add(job);
            }

            user.Jobs = jobs;


            await _userRepository.AddUser(user);
            await _userRepository.SaveChanges();

            return Ok(_mapper.Map<UserDto>(user));
        }

        // Take a string, hash it and return the hashed string and the hashing key
        private ValueTuple<byte[], byte[]> GetHashAndKey(string toHash)
        {
            var hmac = new HMACSHA256();
            var result = hmac.ComputeHash(Encoding.UTF8.GetBytes(toHash));

            return (result, hmac.Key);
        }


        [HttpPost("login")]  // Specifies that this endpoint is reachable by a POST request to the endpoint "x/api/login"
        public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
        {
            // Get the user
            var user = await _userRepository.GetUserByEmail(loginDto.Email, trackChanges: false);

            // If a user with this email and password does not exist, return an error
            if (user == null || !PasswordsEqual(user, loginDto.Password))
                return BadRequest("The provided email or password was wrong");

            return Ok(_mapper.Map<UserDto>(user));
        }

        // Compate a user's password with a raw password
        private bool PasswordsEqual(User user, string rawPassword)
        {
            var rawPasswordHashed = GetHash(rawPassword, user.PasswordSeed);

            return user.Password.SequenceEqual(rawPasswordHashed);
        }

        // Get the hashed value from a string with an existing key
        private byte[] GetHash(string toHash, byte[] key)
        {
            var hmac = new HMACSHA256(key);
            var result = hmac.ComputeHash(Encoding.UTF8.GetBytes(toHash));

            return result;
        }
    }
}