using System;
using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Interfaces;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class QueryValidateCpfHandler : ActionResultRequestHandler<QueryValidateCpf>
    {
        private readonly ICpfValidator _cpfValidator;

        public QueryValidateCpfHandler(ICpfValidator cpfValidator)
        {
            _cpfValidator = cpfValidator;   
        }

        public override Task<IActionResult> Handle(QueryValidateCpf request, CancellationToken cancellationToken)
            => Task.Run(() => 
            {
               var isCpfValid = _cpfValidator.Validate(request.Cpf);

               var response = new ValidatorResponse
               {
                   Type = "cpf",
                   IsValid = isCpfValid,
                   Value = request.Cpf,
                   Formated = isCpfValid ? Convert.ToUInt64(request.Cpf).ToString(@"000\.000\.000\-00") : string.Empty
               };

               return Ok(response);
            });
    }
}