using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using System.Linq;
using System.Threading.Tasks;
using WebImageswap.Models;
using Microsoft.Extensions.Configuration;
using ServiceStack.Redis;

namespace WebImageswap.Services
{
    public class ImageService
    {
        private IHttpContextAccessor _contextAcessor;
        private IConfiguration _configuration;
        public ImageService(IConfiguration configuration, IHttpContextAccessor contextAcessor) {
            _configuration = configuration;
            _contextAcessor = contextAcessor;
        }

        private async Task<bool> IsValid(ImageVO image)
        {
            if(!this.IsValidUrl(image.Source))
                return (await Task.FromResult(false));
            if (!this.IsValidUrl(image.Destination))
                return (await Task.FromResult(false));
            return (await Task.FromResult(true));
        }

        private bool IsValidUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return (false);
            if (!url.Contains(".jpg"))
                return (false);
            return (true);
        }

        public async Task<ImageVO> Create(ImageVO image)
        {
            image.Code = Guid.NewGuid().ToString();
            image.Creation = DateTime.UtcNow;
            image.Deadline = image.Creation.AddHours(image.Hours);
            if (!await this.IsValid(image))
                return (null);
            await this.Save(image);
            image.Url = BuildUrl(image.Code);
            return (await Task.FromResult(image));
        }

        private string BuildUrl(string code)
        {
            string url = _contextAcessor.HttpContext?.Request?.GetDisplayUrl();
            string urlBase = url.Substring(0, url.IndexOf("api"));
            return ($"{urlBase}image/{code}.jpg");
        }

        private async Task<bool> Save(ImageVO image)
        {
            string connectionString = this._configuration["ConnectionString"];
            var manager = new RedisManagerPool(connectionString);
            bool saved = false;
            using (var client = manager.GetClient())
            {
                saved = client.Set<ImageVO>(image.Code, image);
            }
            return (await Task.FromResult(saved));
        }
    }
}
