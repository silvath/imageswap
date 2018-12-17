using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebImageswap.Models;
using WebImageswap.Services;

namespace WebImageswap.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ImageController : Controller
    {
        private ImageService _imageService;
        public ImageController(ImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpPost]
        [ProducesResponseType(typeof(ImageVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> Create([FromBody] ImageVO image)
        {
            try
            {
                return (Ok(await _imageService.Create(image)));
            }
            catch (Exception e)
            {
                return (StatusCode((int)HttpStatusCode.InternalServerError, e));
            }
        }
    }
}
