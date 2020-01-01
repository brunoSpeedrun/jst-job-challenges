using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public abstract class ActionResultRequestHandler<TRequest> : 
        IRequestHandler<TRequest, IActionResult> where TRequest : IRequest<IActionResult>
    {
        public abstract Task<IActionResult> Handle(TRequest request, CancellationToken cancellationToken);

        public ValidationProblemDetails ToValidationProblemDetails(IdentityResult identityResult, int statusCode)
        {
            var validationProblemDetails = new ValidationProblemDetails
            {
                Status = statusCode
            };

            identityResult.Errors
                          .GroupBy(e => e.Code)
                          .ToList()
                          .ForEach(i => validationProblemDetails.Errors.Add(i.Key, i.Select(x => x.Description).ToArray()));

            return validationProblemDetails;
        }

        public IActionResult NotFound() => new NotFoundResult();

        public IActionResult Unauthorized() => new UnauthorizedResult();

        public IActionResult Ok(object value) => new OkObjectResult(value);

        public IActionResult NoContent() => new NoContentResult();

        public IActionResult Created(string location, object value) => new CreatedResult(location, value);

        public IActionResult BadRequest(object value) => new BadRequestObjectResult(value);

        public IActionResult InternalServerError(object value) => new ObjectResult(value)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}