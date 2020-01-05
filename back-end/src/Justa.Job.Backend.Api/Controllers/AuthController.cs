using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Application.Services.Jwt.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{
    [Route("auth")]
    public class AuthController : JustaApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        /// Generate a authentication token.
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>
        ///Sample request:
        /// 
        ///     POST /auth/authorize
        ///     {
        ///         "userName": "fulano.silva",
        ///         "password: "asf2!312"
        ///     }
        /// </remarks>
        /// <returns>A authentication token</returns>
        /// <response code="200">A authentication token</response>
        /// <response code="401">UserName or Password incorrenct.</response>
        [HttpPost("authorize")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(JsonWebToken), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Authorize([FromBody]AuthenticateUserRequest request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }
    }
}