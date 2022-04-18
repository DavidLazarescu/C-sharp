using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.Extensions.Logging;
using backend_learning.Infrastructure.DTOs.User;
using backend_learning.Infrastructure.Interfaces;
using backend_learning.Domain.Entities;
using Microsoft.AspNetCore.JsonPatch;


namespace backend_learning.Controllers;

// A controller is basically dealing with what happens when a user connects to a specified endpoint.
[Route("api/users")]  // This is specifying a default endpoint for the class, which can be changed by every method
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;


    public UserController(IUserRepository userRepository, ILogger<UserController> logger, IMapper mapper)  // Getting "userRepository" by Dependency Injection
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }


    // This GET method is connected to the endpoint "api/users" because it didn't specify any, so it uses the classes default one.
    // It gets called and executed everytime someone calls: "localhost:xxxx/api/users"
    [HttpGet]  // the "HttpGet" attribute specifies that the method is reacting to a get request
    public async Task<ActionResult<IEnumerable<UserOutputDto>>> GetAllUsers()
    {
        return Ok(await _userRepository.GetAllUserDtosAsync(trackChanges: false));  // Returning all users
    }

    // This is also a GET method but this time it is connected to the endpoint "api/users/email", also by putting "{email}"
    // into curly braces, you give it as a parameter to the method itself, so calling: "localhost:xxxx/api/users/testman@gmail.com"
    // would call this method with the argument "testman@gmail.com"
    [HttpGet("{email}")]
    public async Task<ActionResult<UserOutputDto>> GetUser(string email)
    {
        return Ok(await _userRepository.GetUserDtoByEmailAsync(email, trackChanges: false));  // Returning user with the email
    }


    [HttpDelete("delete/{email}")]
    public async Task<ActionResult<UserOutputDto>> DeleteUser(string email)
    {
        User user = await _userRepository.GetUserByEmailAsync(email, trackChanges: true);
        if (user == null)
        {
            _logger.LogInformation("User with email: " + email + " does not exist");
            return BadRequest("A user with this email does not exist");
        }

        _userRepository.Delete(user);
        
        var userResponseDto = _mapper.Map<UserOutputDto>(user);
        await _userRepository.SaveChangesAsync();

        return Ok(userResponseDto);
    }

    [HttpPatch("{email}")]
    public async Task<ActionResult> PartiallyUpdateUser(string email, [FromBody] JsonPatchDocument<UserForUpdateDto> patchDoc)
    {
        if (patchDoc == null)
        {
            _logger.LogError("Partial update failed, invalid request");
            return BadRequest("Invalid request");
        }

        User user = await _userRepository.GetUserByEmailAsync(email, trackChanges: true);

        if (user == null)
        {
            _logger.LogInformation("User with email: " + email + " does not exist");
            return BadRequest("A user with this email does not exist");
        }

        UserForUpdateDto userToPatch = _mapper.Map<UserForUpdateDto>(user);

        // There usually just is validation on objects which are coming from the request directly (e.g. [FromBody] LoginDto loginDto).
        // To also apply validation to this patch request, you need to add "ModelState", which handles the state of the model creation, to the ApplyTo()
        // method. When including it in the method call, the ApplyTo() method keeps track of the model state
        patchDoc.ApplyTo(userToPatch, ModelState);

        // To validate the model state, use TryValidateModel()
        TryValidateModel(userToPatch);

        // Handle what happens if the model state is invalid (The created model doesnt fit with the data validation (by data annotations or fluentAPI))
        if(!ModelState.IsValid)
        {
            _logger.LogWarning("Patch request failed due to invalid data");
            return BadRequest(ModelState);
        }

        _mapper.Map(userToPatch, user);

        await _userRepository.SaveChangesAsync();

        return NoContent();
    }
}