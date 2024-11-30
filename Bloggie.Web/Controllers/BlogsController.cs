using Bloggie.Web.Models.DataTransfers;
using Bloggie.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IBlogPostLikeRepository blogPostLikeRepository;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        public BlogsController(IBlogPostRepository _blogPostRepository, IBlogPostLikeRepository _blogPostLikeRepository, UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> _signInManager)
        {
            blogPostRepository = _blogPostRepository;
            blogPostLikeRepository = _blogPostLikeRepository;
            userManager = _userManager;
            signInManager = _signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urlHandle)
        {
            bool isLiked = false;
            var blogpost = await blogPostRepository.GetBlogPostByUrlHandleAsync(urlHandle);
            var blogpostDetailViewModel = new BlogPostDetailViewModel();

            if (blogpost is not null)
            {
                //Sirf Ye check krne ke liye ke jo user signed in hai kya usne blog ko like kra hua hai ya nhi?
                if(signInManager.IsSignedIn(User))
                {
                    //Get all like of this blog for the user
                    var allLikesfortheBlog = await blogPostLikeRepository.GetAllLikesForBlogForUser(blogpost.Id);

                    //Is there any like for the above blog against the signedIn User 
                    var userId = userManager.GetUserId(User);

                    if(userId != null)
                    {
                        isLiked = allLikesfortheBlog.Any(x => x.UserId == Guid.Parse(userId));                        
                    }
                }


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
                blogpostDetailViewModel.isLikedbyCurrentUser = isLiked;
                
            }
            return View(blogpostDetailViewModel);
        }
    }
}
