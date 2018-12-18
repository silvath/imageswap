using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Extensions;
using System.Linq;
using System.Threading.Tasks;
using WebImageswap.Models;
using Microsoft.Extensions.Configuration;

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

        public async Task<ImageVO> Create(ImageVO image)
        {
            image.Code = Guid.NewGuid().ToString();
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
            //TODO: Work over here

            return (await Task.FromResult(true));
        }
    }
}
