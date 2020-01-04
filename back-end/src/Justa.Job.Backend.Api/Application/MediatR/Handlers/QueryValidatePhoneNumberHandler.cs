using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Application.Services.DataValidation;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class QueryValidatePhoneNumberHandler : ActionResultRequestHandler<QueryValidatePhoneNumber>
    {
        private PhoneNumberValidatorService _phoneNumberValidator;

        public QueryValidatePhoneNumberHandler(PhoneNumberValidatorService phoneNumberValidator)
        {
            _phoneNumberValidator = phoneNumberValidator;
        }

        public override async Task<IActionResult> Handle(QueryValidatePhoneNumber request, CancellationToken cancellationToken)
        {
            var phoneNumberValidatorResponse = await _phoneNumberValidator.ValidateAsync(request.Number);

            return Ok(phoneNumberValidatorResponse);
        }
    }
}