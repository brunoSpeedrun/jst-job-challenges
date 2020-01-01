using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Justa.Job.Backend.Api;
using Justa.Job.Backend.Api.Application.Services.Jwt.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Justa.Job.Backend.Test
{
    public class JustaJobBackendTestBase : IDisposable
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _adminUserName = "admin";
        protected readonly string _adminPassword = "admin@123";

        public JustaJobBackendTestBase()
        {
            var webHostBuilder = new WebHostBuilder()
                                    .UseEnvironment("Production")
                                    .UseStartup<Startup>()
                                    .ConfigureAppConfiguration(config => 
                                    {
                                        config.SetBasePath(Directory.GetCurrentDirectory())
                                              .AddJsonFile("appsettings.json");
                                    });
            var server = new TestServer(webHostBuilder);
            _httpClient = server.CreateClient();
        }

        protected Task<JsonWebToken> GetJwt()
            => Task.Run(async () => 
            {
                var adminUser = new 
                {
                    UserName = _adminUserName,
                    Password = _adminPassword
                };

                var stringContent = ToStringContentApplicationJson(adminUser);

                using (var httpResponse = await _httpClient.PostAsync("/auth/authorize", stringContent))
                {
                    var httpContent = await httpResponse.Content.ReadAsStringAsync();
                    var jwt = JsonConvert.DeserializeObject<JsonWebToken>(httpContent);
                    return jwt;   
                }
            });

        protected StringContent ToStringContentApplicationJson(object value)
        {
            var jsonContent = JsonConvert.SerializeObject(value); 
            var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return stringContent;
        }
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}