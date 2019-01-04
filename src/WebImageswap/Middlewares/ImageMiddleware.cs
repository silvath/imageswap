using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using WebImageswap.Models;

namespace WebImageswap.Middlewares
{
    public class ImageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public ImageMiddleware(RequestDelegate next, IHostingEnvironment env, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this._next = next;
            this._configuration = configuration;
            this._httpClientFactory = httpClientFactory;
        }
        public async Task Invoke(HttpContext context)
        {
            string imageCode = await this.GetImageCode(context);
            if (!string.IsNullOrEmpty(imageCode))
            {
                byte[] imageBody = await GetImageBody(imageCode);
                context.Response.OnStarting(state =>
                {
                    var httpContext = (HttpContext)state;
                    httpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    httpContext.Response.Headers.Add("Content-Type", new[] { "image/jpeg" });
                    using (BinaryWriter writer = new BinaryWriter(httpContext.Response.Body))
                    {
                        if (imageBody != null)
                            writer.Write(imageBody);
                        writer.Flush();
                    }
                    return Task.FromResult(0);
                }, context);
            }
            else
            {
                await _next.Invoke(context);
            }
        }

        private async Task<string> GetImageCode(HttpContext context)
        {
            string path = context.Request.Path.Value.ToLower();
            int index = path.IndexOf("/image/");
            if (index < 0)
                return (await Task.FromResult<string>(string.Empty));
            int start = index + 7;
            return (path.Substring(start, path.Length - (start + 4)));
        }

        private async Task<byte[]> GetImageBody(string code)
        {
            string connectionString = this._configuration["ConnectionString"];
            var manager = new RedisManagerPool(connectionString);
            ImageVO image = null;
            using (var connection = manager.GetClient())
            {
                image = connection.Get<ImageVO>(code);
            }
            if (image == null)
                return (await Task.FromResult<byte[]>(null));
            string url = image.Deadline < DateTime.UtcNow ? image.Destination : image.Source;
            var client = _httpClientFactory.CreateClient();
            byte[] body = await client.GetByteArrayAsync(url);
            return (body);
        }
    }
}
