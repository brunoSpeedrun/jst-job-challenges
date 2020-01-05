using System;
using System.Threading;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.MediatR.Requests;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Interfaces;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Justa.Job.Backend.Api.Application.MediatR.Handlers
{
    public class QueryValidateCnpjHandler : ActionResultRequestHandler<QueryValidateCnpj>
    {
        private readonly ICnpjValidator _cnpjValidator;

        public QueryValidateCnpjHandler(ICnpjValidator cnpjValidator)
        {
            _cnpjValidator = cnpjValidator;   
        }

        public override Task<IActionResult> Handle(QueryValidateCnpj request, CancellationToken cancellationToken)
            => Task.Run(() => 
            {
                var isCnpjValid = _cnpjValidator.Validate(request.Cnpj);

                var response = new ValidatorResponse
                {
                    Type = "cpf",
                    IsValid = isCnpjValid,
                    Value = request.Cnpj,
                    Formated = isCnpjValid ? Convert.ToUInt64(request.Cnpj).ToString(@"00\.000\.000\/0000\-00") : string.Empty
                };

                return Ok(response);
            });
    }
}