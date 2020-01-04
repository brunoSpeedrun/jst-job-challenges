using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Requests
{
    public class QueryValidatePhoneNumber : IRequest<IActionResult>
    {
        [FromRoute(Name = "number")]
        public string Number { get; set; }
    }
}