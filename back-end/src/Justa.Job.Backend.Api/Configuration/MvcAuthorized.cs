using System;
using Justa.Job.Backend.Api.Application.Services.Jwt.Models;
using Justa.Job.Backend.Api.Identity.Data;
using Justa.Job.Backend.Api.Identity.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Justa.Job.Backend.Api.Configuration
{
    public static class MvcAuthorized
    {
        public static void AddAuthorizedMvc(this IServiceCollection services)
        {
            AddIdentityCore(services);
            AddJwtAuthorization(services);
            AddAuthenticatedUser(services);
        }

        private static void AddIdentityCore(IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")
                                    ?? "host=localhost;port=5432;database=ApplicationDbContext;username=jst;password=jst";

            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();
        }

        private static void AddAuthenticatedUser(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Infra - Identity
            services.AddScoped<IAuthenticatedUser, AuthenticatedUser>();
        }

        private static void AddJwtAuthorization(IServiceCollection services)
        {
            var jwtSettings = services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = jwtSettings.SigningCredentials.Key
                };
            });
        }
    }
}