using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Application.Services.Jwt.Interfaces;
using Justa.Job.Backend.Api.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserRequest, IActionResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;

        public AuthenticateUserHandler(UserManager<ApplicationUser> userManager, 
                                       IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<IActionResult> Handle(AuthenticateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user is null)
            {
                var notFound = new NotFoundResult();
                return notFound;
            }

            var passwordIsValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!passwordIsValid)
            {
                var unauthorized = new UnauthorizedResult();
                return unauthorized;
            }

            var jwt = _jwtService.CreateJsonWebToken(user);

            return new OkObjectResult(jwt);
        }
    }
}