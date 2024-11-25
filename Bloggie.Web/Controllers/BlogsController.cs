using Bloggie.Web.Models.DataTransfers;
using Bloggie.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        
        public BlogsController(IBlogPostRepository _blogPostRepository, IBlogPostLikeRepository _blogPostLikeRepository)
        {
            blogPostRepository = _blogPostRepository;
            blogPostLikeRepository = _blogPostLikeRepository;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            var blogpost = await blogPostRepository.GetBlogPostByUrlHandleAsync(urlHandle);
            var blogpostDetailViewModel = new BlogPostDetailViewModel();

            if (blogpost is not null)
            {
                var totalLikes = await blogPostLikeRepository.GetTotalLikes(blogpost.Id);

                blogpostDetailViewModel.Id = blogpost.Id;
                blogpostDetailViewModel.Heading = blogpost.Heading;
                blogpostDetailViewModel.PageTitle = blogpost.PageTitle;
                blogpostDetailViewModel.Content = blogpost.Content;
                blogpostDetailViewModel.PublishedDate = blogpost.PublishedDate;
                blogpostDetailViewModel.Author = blogpost.Author;
                blogpostDetailViewModel.FeaturedImageUrl = blogpost.FeaturedImageUrl;
                blogpostDetailViewModel.UrlHandle = blogpost.UrlHandle;
                blogpostDetailViewModel.ShortDescription = blogpost.ShortDescription;
                blogpostDetailViewModel.Visible = blogpost.Visible;
                blogpostDetailViewModel.Tags = blogpost.Tags;
                blogpostDetailViewModel.TotalLikes = totalLikes;
                
            }
            return View(blogpostDetailViewModel);
        }
    }
}
