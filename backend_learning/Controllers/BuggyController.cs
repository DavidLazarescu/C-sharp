using Microsoft.AspNetCore.Mvc;


namespace backend_learning.Controllers
{
    [Route("api/buggy")]
    public class BuggyController : BaseController
    {
        [HttpGet]
        public void ThrowException()
        {
            string s = null;
            s.Append('a');
        }
    }
}