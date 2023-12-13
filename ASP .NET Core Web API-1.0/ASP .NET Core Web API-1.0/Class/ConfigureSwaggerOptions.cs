using ASP_.NET_Core_Web_API_1._0.Model;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ASP_.NET_Core_Web_API_1._0.Class
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Title = "ASP .NET Core Web API",
                Version = description.ApiVersion.ToString()
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        private static void AddSecurityDefinitionAndRequirements(OpenApiSettings openApiSettings, SwaggerGenOptions options)
        {
            if (openApiSettings.Security != null)
            {
                options.AddSecurityDefinition(openApiSettings.Security.Scheme,
                    new OpenApiSecurityScheme
                    {
                        Description = openApiSettings.Security.Description,
                        Name = openApiSettings.Security.Name,
                        In = ParameterLocation.Header,
                        Scheme = "Bearer",
                        Type = openApiSettings.Security.SchemeType,
                        BearerFormat = openApiSettings.Security.BearerFormat
                    });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {Type = ReferenceType.SecurityScheme, Id = openApiSettings.Security.Scheme}
                        },
                        new List<string>()
                    }
                });
            }
        }
    }
}
