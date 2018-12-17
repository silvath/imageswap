using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebImageswap.Models;

namespace WebImageswap.Services
{
    public class ImageService
    {
        public async Task<ImageVO> Create(ImageVO image)
        {
            //TODO: Work over here
            return (await Task.FromResult(image));
        }
    }
}
