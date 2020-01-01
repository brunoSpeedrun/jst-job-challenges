using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Requests
{
    public class QueryUserByUserName : IRequest<IActionResult>
    {
        [FromRoute(Name = "userName")]
        public string UserName { get; set; }
    }
}