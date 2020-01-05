using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Justa.Job.Backend.Api.Configuration
{
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c => 
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Data Validation Api", 
                    Version = "v1",
                    Description = "Perform email, phone number, cpf, cnpj, and other validations",
                    Contact = new OpenApiContact
                    {
                        Name = "Bruno Xavier",
                        Email = "bruno.xavier.moura@gmail.com",
                        Url = new Uri("https://github.com/brunoSpeedrun")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://github.com/brunoSpeedrun/jst-job-challenges/LICENSE")
                    } 
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"
JWT Authorization header using the Bearer scheme. 
                                   
Enter 'Bearer' [space] and then your token in the text input below.
                                   
Example: Bearer 12345abcdef",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);   
                }
            });
        }
    }
}