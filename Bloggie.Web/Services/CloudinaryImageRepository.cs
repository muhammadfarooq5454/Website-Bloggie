using Microsoft.EntityFrameworkCore;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Net;

namespace Bloggie.Web.Services
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration _configuration;
        private readonly Account account;

        public CloudinaryImageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            account = new Account(_configuration.GetSection("Cloudinary")["CloudName"],
                                  _configuration.GetSection("Cloudinary")["ApiKey"], 
                                  _configuration.GetSection("Cloudinary")["ApiSecret"]);
        }

        //Using Cloudinary as Cloud Service
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                //Read Files From Upload Button
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };
            
            var uploadResult = await client.UploadAsync(uploadParams);

            if(uploadResult is not null && uploadResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                return uploadResult.SecureUrl.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
