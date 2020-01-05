using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    public class JustaApiController : ControllerBase
    {
        protected readonly IMediator _mediator;

        public JustaApiController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}