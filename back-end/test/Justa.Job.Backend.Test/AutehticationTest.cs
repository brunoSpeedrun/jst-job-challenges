using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Justa.Job.Backend.Api.Application.Services.Jwt.Models;
using Newtonsoft.Json;
using Xunit;

namespace Justa.Job.Backend.Test
{
    public class AutehticationTest : JustaJobBackendTestBase
    {
        [Fact]
        public void ShouldBeUnauthorized()
        {
            var httpResponse = _httpClient.GetAsync("/users").Result;
            
            Assert.True(httpResponse.StatusCode == HttpStatusCode.Unauthorized);   
        }

        [Fact]
        public async Task ShouldGetAuthenticationToken()
        {
            var jsonContent = JsonConvert.SerializeObject(new { Username = "admin", Password = "admin@123" }); 
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var httpResponse = await _httpClient.PostAsync("/auth/authorize", stringContent);
            
            Assert.True(httpResponse.StatusCode == HttpStatusCode.OK);

            var httpContent = await httpResponse.Content.ReadAsStringAsync();
            var jwt = JsonConvert.DeserializeObject<JsonWebToken>(httpContent);

            Assert.NotNull(jwt.AccessToken);
        }
    }
}