using Application.Common.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Common.RequestParameters;
using Application.Common.Exceptions;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> GetUserByEmail(string email)
        {
            if (email == null)
            {
                _logger.LogWarning("Getting user by email failed due to the email parameter being null");
                return BadRequest("You need to specify an email");
            }

            return await _userService.GetUserByEmailAsync(email);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserRequestParameter requestParameter)
        {
            if (requestParameter == null)
            {
                _logger.LogWarning("Getting users failed due to the request parameters being null");
                return BadRequest("Invalid request parameters");
            }

            try
            {
                var result = await _userService.GetUsersAsync(requestParameter);
                return Ok(result);
            }
            catch (InvalidParameterException e)
            {
                _logger.LogWarning("Getting users failed: " + e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("{email}")]
        public async Task<ActionResult> PatchUser(string email, [FromBody] JsonPatchDocument<UserUpdateDto> patchDoc)
        {
            if (email == null)
            {
                _logger.LogWarning("Patching the user failed due to the email parameter being null");
                return BadRequest("You need to specify an email");
            }
            if (patchDoc == null)
            {
                _logger.LogWarning("Patching the user failed due to the patchDoc parameter being null");
                return BadRequest("You need to specify user data");
            }

            try
            {
                await _userService.PatchUserAsync(email, patchDoc, this);
                return NoContent();
            }
            catch (InvalidParameterException e)
            {
                _logger.LogWarning("Getting users failed: " + e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            try
            {
                await _userService.RegisterUserAsync(registerDto);
                return CreatedAtRoute("Register", registerDto);
            }
            catch (InvalidParameterException e)
            {
                _logger.LogInformation("Registering user failed: " + e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}