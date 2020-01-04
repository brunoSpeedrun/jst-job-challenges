using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Requests
{
    public class QueryValidateCnpj : IRequest<IActionResult>
    {
        [FromRoute(Name = "cnpj")]
        public string Cnpj { get; set; }
    }
}