using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class UpdateUserHandler : ActionResultRequestHandler<UpdateUserRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UpdateUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<IActionResult> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.UserName);

            if (applicationUser is null)
            {
                return NotFound();
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
                
                return Ok(updatedUser);
            }

            var validationProblemDetails = ToValidationProblemDetails(identityResult, StatusCodes.Status500InternalServerError);

            return InternalServerError(validationProblemDetails);
        }
    }
}