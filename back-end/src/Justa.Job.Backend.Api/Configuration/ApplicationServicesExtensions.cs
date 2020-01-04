using Justa.Job.Backend.Api.Application.Services.DataValidation;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Interfaces;
using Justa.Job.Backend.Api.Application.Services.Jwt;
using Justa.Job.Backend.Api.Application.Services.Jwt.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Justa.Job.Backend.Api.Configuration
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<ICpfValidator, InMemoryCpfValidator>();
        }
    }
}