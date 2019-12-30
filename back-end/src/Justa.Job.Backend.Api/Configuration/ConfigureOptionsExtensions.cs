using Justa.Job.Backend.Api.Application.Services.Jwt.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Justa.Job.Backend.Api.Configuration
{
    public static class ConfigureOptionsExtensions
    {
        public static void ConfigureApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        }
    }
}