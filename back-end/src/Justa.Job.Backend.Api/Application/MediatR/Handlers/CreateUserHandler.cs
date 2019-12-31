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
    public class CreateUserHandler : IRequestHandler<CreateUserRequest, IActionResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUserHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var applicationUser = new ApplicationUser
            {
                Email = request.Email,
                EmailConfirmed = true,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var identityResult = await _userManager.CreateAsync(applicationUser, request.Password);

            if (identityResult.Succeeded)
            {
                var createdResult = new CreatedResult($"users/{applicationUser.UserName}", new 
                {
                    request.UserName,
                    request.Email,
                    request.PhoneNumber
                });

                return createdResult;                
            }

            var validationProblemDetails = new ValidationProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest
            };

            identityResult.Errors
                          .GroupBy(e => e.Code)
                          .ToList()
                          .ForEach(i => validationProblemDetails.Errors.Add(i.Key, i.Select(x => x.Description).ToArray()));

            return new BadRequestObjectResult(validationProblemDetails);
        }
    }
}