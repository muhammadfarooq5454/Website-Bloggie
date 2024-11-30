﻿using Bloggie.Web.Models.DataTransfers;
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
        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            _blogPostLikeRepository = blogPostLikeRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddLike([FromBody] AddLikeBlogPostRequest addLikeBlogPostRequest)
        {
            var blogPostLike = new BlogPostLike()
            {
                BlogPostId = addLikeBlogPostRequest.BlogPostId,
                UserId = addLikeBlogPostRequest.UserId
            };

            await _blogPostLikeRepository.AddLikeForBlog(blogPostLike);
            return Ok();
        }

        [HttpGet]
        [Route("TotalLikes/{blogPostId:guid}")]
        public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId)
        {
           var totalLikes = await _blogPostLikeRepository.GetTotalLikes(blogPostId);
           return Ok(totalLikes);
        }
    }
}
