using Bloggie.Web.Data;
using Bloggie.Web.Models;
using Bloggie.Web.Models.DataTransfers;
using Bloggie.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Bloggie.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly ITagRepository _tagRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _tagRepository = tagRepository;
        }

        public async Task<IActionResult> Index()
        {
            //getting all blogposts
            var allblogPosts = await _blogPostRepository.GetAllBlogPostsAsync();

            //getting all tags
            var tags = await _tagRepository.GetAllTagsAsync();


            //combining two models into other model
            var homeViewModel = new HomeViewModel()
            {
                blogPosts = allblogPosts,
                tags = tags
            };


            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
