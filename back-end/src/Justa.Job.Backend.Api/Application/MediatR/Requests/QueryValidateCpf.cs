using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Requests
{
    public class QueryValidateCpf : IRequest<IActionResult>
    {
        [FromRoute(Name = "cpf")]
        public string Cpf { get; set; }
    }
}