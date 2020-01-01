using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, IActionResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public DeleteUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userManager.FindByNameAsync(request.UserName);

            if (userToDelete is null)
            {
                var notFound = new NotFoundResult();
                return notFound;
            }

            var identityResult = await _userManager.DeleteAsync(userToDelete);

            if (!identityResult.Succeeded)
            {
                var noContent = new NoContentResult();
                return noContent;
            }

            var validationProblemDetails = new ValidationProblemDetails
            {
                Status = (int)HttpStatusCode.InternalServerError
            };

            identityResult.Errors
                          .GroupBy(e => e.Code)
                          .ToList()
                          .ForEach(i => validationProblemDetails.Errors.Add(i.Key, i.Select(x => x.Description).ToArray()));

            var objectResult = new ObjectResult(validationProblemDetails)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            return objectResult;
        }
    }
}