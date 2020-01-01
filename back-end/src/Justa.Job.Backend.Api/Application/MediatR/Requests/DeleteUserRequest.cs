using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Requests
{
    public class DeleteUserRequest : IRequest<IActionResult>
    {
        [FromRoute(Name = "userName")]
        public string UserName { get; set; }
    }
}