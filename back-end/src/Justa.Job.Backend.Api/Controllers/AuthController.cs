using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{
    [Route("[controller]")]
    public class AuthController : JustaApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("authorize")]
        [AllowAnonymous]
        public async Task<IActionResult> Authorize([FromBody]AuthenticateUserRequest request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }
    }
}