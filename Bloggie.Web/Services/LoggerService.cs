using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Services
{
    public class LoggerService
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public LoggerService(BloggieDbContext bloggieDbContext)
        { 
            _bloggieDbContext = bloggieDbContext;
        }

        public async Task LoggerAsync(string message)
        {
            var errorException = new ErrorException()
            {
                Message = message
            };

            await _bloggieDbContext.ErrorExceptions.AddAsync(errorException);
            await _bloggieDbContext.SaveChangesAsync();
        }
    }
}
