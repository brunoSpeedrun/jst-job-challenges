using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class ChangeUserPasswordHandler : ActionResultRequestHandler<ChangeUserPasswordRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ChangeUserPasswordHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<IActionResult> Handle(ChangeUserPasswordRequest request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.UserName);

            if (applicationUser is null)
            {
                return NotFound();
            }

            var identityResult = await _userManager.ChangePasswordAsync(applicationUser, request.CurrentUserPassword, request.NewPassword);

            if (identityResult.Succeeded)
            {
                return NoContent();
            }

            var validationProblemDetails = ToValidationProblemDetails(identityResult, StatusCodes.Status400BadRequest);

            return BadRequest(validationProblemDetails);
        }
    }
}