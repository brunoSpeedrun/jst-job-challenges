using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : JustaApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }   

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        [HttpGet("{userName}")]
        public IActionResult GetUser(string userName)
        {
            return Ok(new 
            {
                Message = "It's Works"
            });
        }
    }
}