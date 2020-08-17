using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MsCoreOne.Configs;
using MsCoreOne.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace MsCoreOne.Extensions
{
    public static class SwaggerExtension
    {
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(c =>
            {
                // add a custom operation filter which sets default value
                c.OperationFilter<SwaggerDefaultValuesOperationFilter>();

                c.OperationFilter<AuthResponsesOperationFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri("/connect/token", UriKind.Relative),
                            AuthorizationUrl = new Uri("/connect/authorize", UriKind.Relative),
                            Scopes = new Dictionary<string, string> { { "api.mscoreone", "MsCoreOne Api" } }
                        }
                    }
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = "Bearer"}
                        },
                        new List<string> { "api.mscoreone" }
                    }
                });
            });
        }
    }
}
