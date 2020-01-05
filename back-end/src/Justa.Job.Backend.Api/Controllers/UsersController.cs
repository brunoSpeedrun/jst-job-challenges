using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{
    [Route("users")]
    public class UsersController : JustaApiController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }   

        /// <summary>
        /// Create a user.
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// 
        ///     POST /users
        ///     {
        ///         "userName": "fulano.silva",
        ///         "password: "asf2!312",
        ///         "email: "fulano.silva@mail.com",
        ///         "phoneNumber": "14158586273"
        ///     }
        /// </remarks>
        /// <returns>The user created.</returns>
        /// <response code="201">The user created.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateUser([FromBody]CreateUserRequest request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        /// <summary>
        /// Get a user.
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// 
        ///     GET users/admin
        /// </remarks>
        /// <returns>A user.</returns>
        /// <response code="200">A user.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpGet("{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUser([FromQuery]QueryUserByUserName request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        /// <summary>
        /// Get users per page.
        /// </summary>
        /// <remarks>
        /// Parameters:
        /// 
        /// Page: Page number ber starting at zero.
        /// PageSize: Number of users per page (max: 50)
        /// SortBy: The field where the sort will be done. (userName, Email or PhoneNumber)
        /// SortDirection: asc or desc
        /// Search: Search term
        /// 
        ///Sample request:
        /// 
        ///     GET /users?search=@mail.com
        /// </remarks>
        /// <returns>The user created.</returns>
        /// <response code="200">The user created.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpGet]
        [Produces("application/hal+json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsers([FromQuery]QueryUserGetPage request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// 
        ///     DELETE users/fulano.silva
        /// </remarks>
        /// <returns>No Content.</returns>
        /// <response code="204">No Content.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpDelete("{userName}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUser([FromQuery]DeleteUserRequest request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// 
        ///     PUT /users/fulano.silva
        ///     {
        ///         "email: "fulano.silva2@mail.com",
        ///         "phoneNumber": "14112345678"
        ///     }
        /// </remarks>
        /// <returns>No Content.</returns>
        /// <response code="200">The user updated.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpPut("{userName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUser([FromRoute]string userName, [FromBody]UpdateUserRequest request)
        {
            request.UserName = userName;

            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        /// <summary>
        /// Change user password.
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// 
        ///     PUT /users/fulano.silva/change-password
        ///     {
        ///         "currentUserPassword: "fulano@123",
        ///         "newPassword": "fulano@1234"
        ///     }
        /// </remarks>
        /// <returns>No Content.</returns>
        /// <response code="200">The user updated.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpPut("{userName}/change-password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdatePassword([FromRoute]string userName, [FromBody]ChangeUserPasswordRequest request)
        {
            request.UserName = userName;

            var actionResult = await _mediator.Send(request);

            return actionResult;
        }
    }
}