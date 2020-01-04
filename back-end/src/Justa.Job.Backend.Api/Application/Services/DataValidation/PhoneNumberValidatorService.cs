using System.Net.Http;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Justa.Job.Backend.Api.Application.Services.DataValidation
{
    public class PhoneNumberValidatorService
    {
        private readonly string _accessKey;
        private readonly HttpClient _httpClient;

        public PhoneNumberValidatorService(HttpClient httpClient, IOptions<DataValidationApiAccessKeys> accessKeys)
        {
            _httpClient = httpClient;
            _accessKey = accessKeys.Value.PhoneNumberAccessKey;
        }

        public async Task<PhoneNumberValidatorResponse> ValidateAsync(string phoneNumber)
        {
            var response = await _httpClient.GetStringAsync($"?access_key={_accessKey}&number={phoneNumber}&format=1");

            var phoneNumberValidatorResponse = JsonConvert.DeserializeObject<PhoneNumberValidatorResponse>(response);

            return phoneNumberValidatorResponse;
        }
    }
}