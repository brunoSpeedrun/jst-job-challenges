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
    public class DeleteUserHandler : ActionResultRequestHandler<DeleteUserRequest>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public DeleteUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<IActionResult> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var userToDelete = await _userManager.FindByNameAsync(request.UserName);

            if (userToDelete is null)
            {
                return NotFound();
            }

            var identityResult = await _userManager.DeleteAsync(userToDelete);

            if (identityResult.Succeeded)
            {
                return NoContent();
            }

            var validationProblemDetails = ToValidationProblemDetails(identityResult, StatusCodes.Status500InternalServerError);

            return InternalServerError(validationProblemDetails);
        }
    }
}