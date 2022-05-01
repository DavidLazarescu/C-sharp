using Application.Common.Dtos;
using Application.Common.Exceptions;
using Application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationService _authenticationService;


        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDto registerDto)
        {
            if (registerDto == null)
            {
                _logger.LogWarning("User registration failed due to the register dto being null");
                return BadRequest("The provided data was invalid");
            }

            try
            {
                await _authenticationService.RegisterUser(registerDto);
                return StatusCode(201);
            }
            catch(InvalidParameterException e)
            {
                _logger.LogWarning("User registration failed: " + e.Message);
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                _logger.LogWarning("User authentication failed due to the register dto being null");
                return BadRequest("The provided data was invalid");
            }

            try
            {
                var token = await _authenticationService.AuthenticateUser(loginDto);
                return Ok(token);
            }
            catch(InvalidParameterException e)
            {
                _logger.LogWarning("User authentication failed: " + e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}