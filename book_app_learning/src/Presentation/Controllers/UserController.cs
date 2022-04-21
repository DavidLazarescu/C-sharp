using Application.Common.Dtos;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Application.Common.RequestParameters;

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


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery] UserRequestParameter requestParameter)
        {
            if(requestParameter == null)
            {
                _logger.LogInformation("Getting users failed due to the request parameters being null");
                return BadRequest("Invalid request parameters");
            }

            return Ok(await _userService.GetUsers(requestParameter));
        }
    }
}