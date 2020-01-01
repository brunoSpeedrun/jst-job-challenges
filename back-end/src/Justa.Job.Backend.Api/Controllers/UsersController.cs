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
        public async Task<IActionResult> GetUser([FromQuery]QueryUserByUserName request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]QueryUserGetPage request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteUser([FromQuery]DeleteUserRequest request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        [HttpPut("{userName}")]
        public async Task<IActionResult> UpdateUser([FromRoute]string userName, [FromBody]UpdateUserRequest request)
        {
            request.UserName = userName;

            var actionResult = await _mediator.Send(request);

            return actionResult;
        }
    }
}