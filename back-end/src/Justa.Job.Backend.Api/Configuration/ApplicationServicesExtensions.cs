using System;
using Justa.Job.Backend.Api.Application.Services.DataValidation;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Interfaces;
using Justa.Job.Backend.Api.Application.Services.DataValidation.Models;
using Justa.Job.Backend.Api.Application.Services.Jwt;
using Justa.Job.Backend.Api.Application.Services.Jwt.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Justa.Job.Backend.Api.Configuration
{
    public static class ApplicationServicesExtensions
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();

            services.AddScoped<ICpfValidator, InMemoryCpfValidator>();
            services.AddScoped<ICnpjValidator, InMemoryCnpjValidator>();
            
            services.AddHttpClient<EmailValidatorService>((serviceProvider, httpClient) => 
            {
                httpClient.BaseAddress = new Uri("http://apilayer.net/api/check");
            });
        }
    }
}