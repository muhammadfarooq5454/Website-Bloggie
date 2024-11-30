﻿using Bloggie.Web.Services;
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
        private readonly LoggerService loggerService;

        public ImagesController(IImageRepository imageRepository, LoggerService loggerService)
        {
            this.imageRepository = imageRepository;
            this.loggerService = loggerService;
        }
      

        [HttpPost]
        //Using IFormFile
        public async Task<IActionResult> UploadAsync(IFormFile file)
        {
            try
            {
                //call a repository
                var imageUrl = await imageRepository.UploadImageAsync(file);

                if(imageUrl == null)
                {
                     return Problem("Something Went Wrong!",null,(int)HttpStatusCode.InternalServerError);
                }
                return new JsonResult(new { link = imageUrl});
            }
            catch(Exception ex)
            {
                await loggerService.LoggerAsync(ex.Message);
                return Problem("An unexpected error occurred.", null, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
