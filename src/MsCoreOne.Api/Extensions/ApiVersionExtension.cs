using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace MsCoreOne.Extensions
{
    public static class ApiVersionExtension
    {
        public static void RegisterApiVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(config =>
            {
                // Specific the default API version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);

                // If the client hasn't specified the API version in the request, 
                // use the default API version number
                config.AssumeDefaultVersionWhenUnspecified = true;

                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;

                // HTTP header based versioning
                config.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            });

            services.AddVersionedApiExplorer(config =>
            {
                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                config.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                config.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
