using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebImageswap.Middlewares
{
    public static class ImageMiddlewareExtension
    {
        public static void AddImage(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));
            //Prerequisites for Image here
        }

        public static IApplicationBuilder UseImage(this IApplicationBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            return (builder.UseMiddleware<ImageMiddleware>());
        }
    }
}
