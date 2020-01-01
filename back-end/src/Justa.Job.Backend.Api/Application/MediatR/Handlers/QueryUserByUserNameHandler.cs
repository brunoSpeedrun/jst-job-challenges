using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class QueryUserByUserNameHandler : ActionResultRequestHandler<QueryUserByUserName>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public QueryUserByUserNameHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task<IActionResult> Handle(QueryUserByUserName request, CancellationToken cancellationToken)
        {
            var applicationUser = await _userManager.FindByNameAsync(request.UserName);

            if (applicationUser is null)
            {
                return NotFound();
            }

            var user = new 
            {
                applicationUser.Id,
                applicationUser.UserName,
                applicationUser.NormalizedUserName,
                applicationUser.Email,
                applicationUser.NormalizedEmail,
                applicationUser.EmailConfirmed,
                applicationUser.PhoneNumber
            };

            return Ok(user);
        }
    }
}