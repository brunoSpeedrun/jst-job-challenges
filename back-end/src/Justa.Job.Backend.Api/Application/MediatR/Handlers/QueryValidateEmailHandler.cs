using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Application.Services.DataValidation;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class QueryValidateEmailHandler : ActionResultRequestHandler<QueryValidateEmail>
    {
        private EmailValidatorService _emailValidator;

        public QueryValidateEmailHandler(EmailValidatorService emailValidator)
        {
            _emailValidator = emailValidator;
        }

        public override async Task<IActionResult> Handle(QueryValidateEmail request, CancellationToken cancellationToken)
        {
            var emailValidatorResponse = await _emailValidator.ValidateAsync(request.Email);

            return Ok(emailValidatorResponse);
        }
    }
}