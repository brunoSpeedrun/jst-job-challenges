using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{

    [Route("validate")]
    public class DataValidationController : JustaApiController
    {
        public DataValidationController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<IActionResult> ValidateCpf([FromQuery]QueryValidateCpf request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        [HttpGet("cnpj/{cnpj}")]
        public async Task<IActionResult> ValidateCnpj([FromQuery]QueryValidateCnpj request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> ValidateEmail([FromQuery]QueryValidateEmail request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }
    }
}