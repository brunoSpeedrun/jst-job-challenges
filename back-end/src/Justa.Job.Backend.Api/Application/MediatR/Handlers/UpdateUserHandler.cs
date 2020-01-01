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
    public class UpdateUserHandler : IRequestHandler<UpdateUserRequest, IActionResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.UserName);

            if (applicationUser is null)
            {
                var notFound = new NotFoundResult();
                return notFound;
            }

            applicationUser.Email = request.Email;
            applicationUser.PhoneNumber = request.PhoneNumber;

            var identityResult = await _userManager.UpdateAsync(applicationUser);

            if (identityResult.Succeeded)
            {
                var updatedUser = new 
                {
                    applicationUser.Id,
                    applicationUser.UserName,
                    applicationUser.NormalizedUserName,
                    applicationUser.Email,
                    applicationUser.NormalizedEmail,
                    applicationUser.EmailConfirmed,
                    applicationUser.PhoneNumber
                };
                
                var okObjectResult = new OkObjectResult(updatedUser);
                return okObjectResult;
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