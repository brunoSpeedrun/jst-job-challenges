using System;
using System.IO;
using System.Net.Http;
using Justa.Job.Backend.Api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace Justa.Job.Backend.Test
{
    public class JustaJobBackendTestBase : IDisposable
    {
        protected readonly HttpClient _httpClient;

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
        public void Dispose()
        {
            _httpClient.Dispose();
        }
    }
}