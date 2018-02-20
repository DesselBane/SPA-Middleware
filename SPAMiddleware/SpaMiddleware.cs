using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
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

        public SpaMiddleware(RequestDelegate next, SpaMiddlewareOptions options, IHostingEnvironment env)
        {
            _next = next;
            _options = SetupPath(env, options);
        }

        #endregion

        private static SpaMiddlewareOptions SetupPath(IHostingEnvironment env, SpaMiddlewareOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.PathToIndex))
                if (File.Exists(options.PathToIndex))
                    return options;

            var indexFile = env.WebRootFileProvider
                .GetDirectoryContents("")
                .FirstOrDefault(x => x.Name.ToLower().Contains("index") && x.Name.ToLower().EndsWith("html"));
            
            if(indexFile != null)
                return new SpaMiddlewareOptions(indexFile.PhysicalPath,options.SpecialRoutes);
            
            throw new ArgumentException("PathToIndex was incorrect.");
        }
        
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