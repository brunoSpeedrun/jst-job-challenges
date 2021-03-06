using Justa.Job.Backend.Api.Application.Services.DataValidation.Models;
using Justa.Job.Backend.Api.Application.Services.Jwt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Justa.Job.Backend.Api.Configuration
{
    public static class ConfigureOptionsExtensions
    {
        public static void ConfigureApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));

            services.Configure<IdentityOptions>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";                // Default Password settings.                
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });

            services.Configure<DataValidationApiAccessKeys>(configuration.GetSection(nameof(DataValidationApiAccessKeys)));
        }
    }
}