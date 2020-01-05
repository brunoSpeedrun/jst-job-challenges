using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Controllers
{

    [Route("validate")]
    public class DataValidationController : JustaApiController
    {
        public DataValidationController(IMediator mediator) : base(mediator)
        {
        }


        /// <summary>
        /// Validate a CPF.
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// 
        ///     GET /validate/cpf/12345678912
        /// </remarks>
        /// <returns>CPF validator response.</returns>
        /// <response code="200">CPF validator response.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType(typeof(ValidatorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ValidateCpf([FromQuery]QueryValidateCpf request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        /// <summary>
        /// Validate a CNPJ.
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// 
        ///     GET /validate/cnpj/01234567891234
        /// </remarks>
        /// <returns>CNPJ validator response.</returns>
        /// <response code="200">CNPJ validator response.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpGet("cnpj/{cnpj}")]
        [ProducesResponseType(typeof(ValidatorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ValidateCnpj([FromQuery]QueryValidateCnpj request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        /// <summary>
        /// Validate a e-mail.
        /// </summary>
        /// <remarks>
        ///Sample request:
        /// 
        ///     GET /validate/email/4liseteterezinh@completecleaningmaintenance.com
        /// </remarks>
        /// <returns>E-mail validator response.</returns>
        /// <response code="200">E-mail validator response.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpGet("email/{email}")]
        [ProducesResponseType(typeof(EmialValidatorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ValidateEmail([FromQuery]QueryValidateEmail request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }

        /// <summary>
        /// Validate a phone number.
        /// </summary>
        /// <remarks>
        ///Enter the country code followed by the number:
        /// 
        ///Sample request:
        /// 
        ///     GET /validate/phone-number/14158586273
        /// </remarks>
        /// <returns>Phone number validator response.</returns>
        /// <response code="200">Phone number validator response.</response>
        /// <response code="401">Request is not authorized.</response>
        [HttpGet("phone-number/{number}")]
        [ProducesResponseType(typeof(PhoneNumberValidatorResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ValidatePhoneNumber([FromQuery]QueryValidatePhoneNumber request)
        {
            var actionResult = await _mediator.Send(request);

            return actionResult;
        }
    }
}