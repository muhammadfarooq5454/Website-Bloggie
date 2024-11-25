using Bloggie.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }
      

        [HttpPost]
        //Using IFormFile
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            //call a repository
            var imageUrl = await imageRepository.UploadImageAsync(file);

            if(imageUrl == null)
            {
                 return Problem("Something Went Wrong!",null,(int)HttpStatusCode.InternalServerError);
            }
            return new JsonResult(new { link = imageUrl});
        }
    }
}
