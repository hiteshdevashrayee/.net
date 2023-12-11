using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace ASP_.NET_Core_Web_API_1._0.Class
{
    public static class ApiVersioningExtensions
    {
        /// <summary>
        /// Register the SwaggerUI middleware with provided options
        /// </summary>
        public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app, SwaggerUIOptions options)
        {
            return app.UseMiddleware<SwaggerUIMiddleware>(options);
        }

        /// <summary>
        /// Register the SwaggerUI middleware with optional setup action for DI-injected options
        /// </summary>
        public static IApplicationBuilder UseSwaggerUI(
            this IApplicationBuilder app,
            Action<SwaggerUIOptions> setupAction = null)
        {
            SwaggerUIOptions options;
            using (var scope = app.ApplicationServices.CreateScope())
            {
                options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<SwaggerUIOptions>>().Value;
                setupAction?.Invoke(options);
            }

            // To simplify the common case, use a default that will work with the SwaggerMiddleware defaults
            if (options.ConfigObject.Urls == null)
            {
                var hostingEnv = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
                options.ConfigObject.Urls = new[] { new UrlDescriptor { Name = $"{hostingEnv.ApplicationName} v1", Url = "v1/swagger.json" } };
            }

            return app.UseSwaggerUI(options);
        }
    }
}
