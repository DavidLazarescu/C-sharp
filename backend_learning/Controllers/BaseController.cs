using Microsoft.AspNetCore.Mvc;

namespace backend_learning.Controllers
{
    [ApiController]               // Specifies that it is an ApiController, which helps the developer building API's
    [Route("api/[controller]")]   // Setting the route the user has to choose to get to the endpoint, e.g. for "User" localhost:7213/api/user
    
    // A baseclass which inherits it's attributes
    public abstract class BaseController : ControllerBase
    {
    }
}