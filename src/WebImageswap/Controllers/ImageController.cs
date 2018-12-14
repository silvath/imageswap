using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using WebImageswap.Services;

namespace WebImageswap.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ImageController : Controller
    {
        private ImageService _image;
        public ImageController(ImageService image)
        {
            _image = image;
        }
        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<GameInfo>), (int)HttpStatusCode.OK)]
        //[ProducesResponseType(typeof(Exception), (int)HttpStatusCode.InternalServerError)]
        //public async Task<ActionResult> GetGames()
        //{
        //    try
        //    {
        //        return(Ok(await _crawler.Search(null)));
        //    }
        //    catch (Exception e)
        //    {
        //        return (StatusCode((int)HttpStatusCode.InternalServerError, e));
        //    }
        //}
    }
}
