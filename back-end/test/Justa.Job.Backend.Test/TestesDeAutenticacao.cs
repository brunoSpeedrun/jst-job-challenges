using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Justa.Job.Backend.Test
{
    public class TestesDeAutenticacao : JustaJobBackendTestBase
    {
        [Fact]
        public void DeveEstarNaoAutorizado()
        {
            var httpResponse = _httpClient.GetAsync("/users").Result;
            
            Assert.True(httpResponse.StatusCode == HttpStatusCode.Unauthorized);   
        }
    }
}