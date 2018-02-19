using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SPAMiddleware
{
    public class SpaMiddleware
    {
        #region Vars

        private readonly RequestDelegate _next;
        private readonly SpaMiddlewareOptions _options;

        #endregion

        #region Constructors

        public SpaMiddleware(RequestDelegate next, SpaMiddlewareOptions options)
        {
            _next = next;
            _options = options;
        }

        #endregion

        public async Task Invoke(HttpContext context)
        {
            if (!_options.SpecialRoutes.Any(x => context.Request.Path.StartsWithSegments(x)) && !Path.HasExtension(context.Request.Path.Value))
            {
                context.Response.StatusCode = (int) HttpStatusCode.OK;
                await context.Response.WriteAsync(File.ReadAllText(_options.PathToIndex));
            }
            else
            {
                await _next(context);
            }
        }
    }
}