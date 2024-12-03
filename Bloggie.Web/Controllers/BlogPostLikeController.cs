using Bloggie.Web.Models.DataTransfers;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository _blogPostLikeRepository;
        private readonly LoggerService _loggerService;
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository, LoggerService loggerService)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
            _loggerService = loggerService;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeBlogPostRequest addLikeBlogPostRequest)
        {
            try
            {
                var blogPostLike = new BlogPostLike()
                {
                    BlogPostId = addLikeBlogPostRequest.BlogPostId,
                    UserId = addLikeBlogPostRequest.UserId
                };

                await _blogPostLikeRepository.AddLikeForBlog(blogPostLike);
                return Ok();
            }
            catch(Exception ex)
            {
                await _loggerService.LogErrorAsync(ex.Message, ex);
                return Problem();
            }
        }


        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemoveLike([FromBody] RemoveLikeBlogPostRequest removeLikeBlogPostRequest)
        {
            try
            {
                var blogPostLike = new BlogPostLike()
                {
                    UserId = removeLikeBlogPostRequest.UserId,
                    BlogPostId = removeLikeBlogPostRequest.BlogPostId
                };

                await _blogPostLikeRepository.RemoveLikeForBlog(blogPostLike);
                return Ok();
            }
            catch(Exception ex)
            {
                await _loggerService.LogErrorAsync(ex.Message, ex);
                return Problem();
            }
        }

        [HttpGet]
        [Route("TotalLikes/{blogPostId:guid}")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
            try
            {
                var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPostId);
                return Ok(totalLikes);
            }
            catch(Exception ex)
            {
                await _loggerService.LogErrorAsync(ex.Message, ex);
                return Problem();
            }
        }
    }
}
