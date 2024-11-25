namespace Bloggie.Web.Services
{
    public interface IImageRepository
    {
        Task<string> UploadImageAsync(IFormFile file);
    }
}
