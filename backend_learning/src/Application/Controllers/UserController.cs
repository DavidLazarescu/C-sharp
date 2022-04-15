using backend_learning.Infrastructure.DTOs;
using backend_learning.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace backend_learning.Controllers
{
    // A controller is basically dealing with what happens when a user connects to a specified endpoint.
    [Route("api/users")]  // This is specifying a default endpoint for the class, which can be changed by every method
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;


        public UserController(IUserRepository userRepository)  // Getting "userRepository" by Dependency Injection
        {
            _userRepository = userRepository;
        }


        // This GET method is connected to the endpoint "api/users" because it didn't specify any, so it uses the classes default one.
        // It gets called and executed everytime someone calls: "localhost:xxxx/api/users"
        [HttpGet]  // the "HttpGet" attribute specifies that the method is reacting to a get request
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            return Ok(await _userRepository.GetAllUserDtosAsync(trackChanges: false));  // Returning all users
        }

        // This is also a GET method but this time it is connected to the endpoint "api/users/email", also by putting "{email}"
        // into curly braces, you give it as a parameter to the method itself, so calling: "localhost:xxxx/api/users/testman@gmail.com"
        // would call this method with the argument "testman@gmail.com"
        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUser(string email)
        {
            return Ok(await _userRepository.GetUserDtoByEmailAsync(email, trackChanges: false));  // Returning user with the email
        }
    }
}