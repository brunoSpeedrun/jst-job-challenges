using System.Net.Http;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Interfaces;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Justa.Job.Backend.Api.Application.Services.DataValidation
{
    public class EmailValidatorService
    {
        private readonly string _accessKey;
        private readonly HttpClient _httpClient;

        public EmailValidatorService(HttpClient httpClient, IOptions<DataValidationApiAccessKeys> accessKeys)
        {
            _httpClient = httpClient;
            _accessKey = accessKeys.Value.EmailApiAccessKey;
        }

        public async Task<EmialValidatorResponse> ValidateAsync(string email)
        {
            var response = await _httpClient.GetStringAsync($"?access_key={_accessKey}&email={email}&smtp=1&format=1");

            var emailValidatorResponse = JsonConvert.DeserializeObject<EmialValidatorResponse>(response);

            return emailValidatorResponse;
        }
    }
}