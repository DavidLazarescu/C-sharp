using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/buggy")]
    public class BuggyController : ControllerBase
    {
        [HttpGet]
        public void CreateInternalServerError()
        {
            string s = null;
            s.Append('a');
        }
    }
}