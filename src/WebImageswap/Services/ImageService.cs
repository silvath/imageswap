using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebImageswap.Models;

namespace WebImageswap.Services
{
    public class ImageService
    {
        private IHostingEnvironment _env;
        public ImageService(IHostingEnvironment env) {
            _env = env;
        }

        public async Task<ImageVO> Create(ImageVO image)
        {
            image.Code = Guid.NewGuid().ToString();
            //TODO: Insert this object in a repo
            image.Url = BuildUrl(image.Code);
            return (await Task.FromResult(image));
        }

        private string BuildUrl(string code)
        {
            //TODO: Fix url here
            return ($"{_env.WebRootPath}image/{code}.jpg");
        }
    }
}
