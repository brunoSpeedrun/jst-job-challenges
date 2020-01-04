using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Models;
using Newtonsoft.Json;
using Xunit;

namespace Justa.Job.Backend.Test
{
    public class DataValidationTest : JustaJobBackendTestBase
    {
        [Theory]
        [InlineData("00000000000", false)]
        [InlineData("93124123", false)]
        [InlineData("99672059035", true)]
        [InlineData("87827655025", true)]
        public async Task ValidateCpfTheory(string cpf, bool isValid)
        {
            var jwt = await GetJwt();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            using (var httpResponse = await _httpClient.GetAsync($"/validate/cpf/{cpf}"))
            {
                Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);
                
                var httpContent = await httpResponse.Content.ReadAsStringAsync();                
                var response = JsonConvert.DeserializeAnonymousType(httpContent, new 
                {
                   type = string.Empty,
                   isValid = false,
                   value = string.Empty,
                   formated = string.Empty
                });

                Assert.Equal(cpf, response.value);        
                Assert.Equal(isValid, response.isValid);
            }
        }

        [Theory]
        [InlineData("11111111111111", false)]
        [InlineData("93124123314124", false)]
        [InlineData("62735445000177", true)]
        [InlineData("50205502000127", true)]
        public async Task ValidateCnpjTheory(string cnpj, bool isValid)
        {
            var jwt = await GetJwt();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            using (var httpResponse = await _httpClient.GetAsync($"/validate/cnpj/{cnpj}"))
            {
                Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);
                
                var httpContent = await httpResponse.Content.ReadAsStringAsync();                
                var response = JsonConvert.DeserializeAnonymousType(httpContent, new 
                {
                   type = string.Empty,
                   isValid = false,
                   value = string.Empty,
                   formated = string.Empty
                });

                Assert.Equal(cnpj, response.value);        
                Assert.Equal(isValid, response.isValid);
            }
        }

        [Theory]
        [InlineData("9sahrawia_15_207a@grandw88.info", true)]
        [InlineData("fulano.gmail", false)]
        public async Task ValidateEmail(string email, bool isValid)
        {
            var jwt = await GetJwt();

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            using (var httpResponse = await _httpClient.GetAsync($"/validate/email/{email}"))
            {
                Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);
                
                var httpContent = await httpResponse.Content.ReadAsStringAsync();                
                var response = JsonConvert.DeserializeObject<EmialValidatorResponse>(httpContent);

                Assert.Equal(email, response.Email);        
                Assert.Equal(isValid, response.FormatValid);
            }
        }
    }
}