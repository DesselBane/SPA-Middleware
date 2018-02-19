using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace SPAMiddleware
{
    public static class SpaMiddlewareExtensions
    {
        public static IServiceCollection AddSpaMiddleware(this IServiceCollection services, string pathToIndex, IEnumerable<string> specialRoutes = null)
        {
            services.AddSingleton(new SpaMiddlewareOptions(pathToIndex, specialRoutes));

            return services;
        }

        public static IApplicationBuilder UseSpaMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<SpaMiddleware>();

            return app;
        }
    }
}