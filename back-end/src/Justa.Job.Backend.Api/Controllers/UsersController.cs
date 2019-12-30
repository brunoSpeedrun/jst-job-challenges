using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : JustaApiController
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new 
            {
                Message = "It's Works"
            });
        }
    }
}