using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Requests
{
    public class QueryValidateEmail : IRequest<IActionResult>
    {
        [FromRoute(Name = "email")]
        public string Email { get; set; }
    }
}