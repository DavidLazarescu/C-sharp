using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class AppUserController : ControllerBase
    {
        public AppUserController()
        {
            
        }


        [HttpGet]
        public async Task<ActionResult<AppUserDto>> GetAllUsers()
        {
            
        }
    }
}