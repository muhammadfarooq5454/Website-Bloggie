using Bloggie.Web.Models.DataTransfers;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bloggie.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogPostsController : Controller
    {
        //Using DI
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository)
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            //Get Tags from Repository

            var tags  = await _tagRepository.GetAllTagsAsync();

            var model = new AddBlogPostRequest()
            {
                Tags = tags.Select(x => new SelectListItem { Text = x.DisplayName , Value = x.Id.ToString() })
            };

            return View(model);
        }

        //Function Overloading
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            var blogPost = new BlogPost()
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                FeaturedImageUrl = addBlogPostRequest.FeaturedImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                ShortDescription = addBlogPostRequest.ShortDescription,
                Visible = addBlogPostRequest.Visible
            };

            var SelectedTags = new List<Tag>();

            //Map Tags from selected Tags
            foreach (var tagId in addBlogPostRequest.SelectedTags)
            {
                var existingtag = await _tagRepository.GetTagAsync(Guid.Parse(tagId));

                if(existingtag != null)
                {
                    SelectedTags.Add(existingtag);
                }

            }

            blogPost.Tags = SelectedTags;

            await _blogPostRepository.AddBlogPostAsync(blogPost);
            
            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> List()
        {
            IEnumerable<BlogPost> model = await _blogPostRepository.GetAllBlogPostsAsync();

            return View(model);
        }
        
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var blogPost = await _blogPostRepository.GetBlogPostAsync(id);
            var alltags = await _tagRepository.GetAllTagsAsync();

            if (blogPost is not null)
            {
                var editblogpostrequest = new EditBlogPostRequest()
                {
                    Id = blogPost.Id,
                    Heading = blogPost.Heading,
                    PageTitle = blogPost.PageTitle,
                    Content = blogPost.Content,
                    UrlHandle = blogPost.UrlHandle,
                    PublishedDate = blogPost.PublishedDate,
                    Author = blogPost.Author,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    Visible = blogPost.Visible,
                    ShortDescription = blogPost.ShortDescription,

                    Tags = alltags.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }),

                    SelectedTags = blogPost.Tags.Select(x => x.Id.ToString()).ToList()
                };
                return View(editblogpostrequest);
            }
            return View(null);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditBlogPostRequest editBlogPostRequest)
        {
            var existingblogPost = await _blogPostRepository.GetBlogPostAsync(editBlogPostRequest.Id);

            existingblogPost.Heading = editBlogPostRequest.Heading;
            existingblogPost.PageTitle = editBlogPostRequest.PageTitle;
            existingblogPost.Content = editBlogPostRequest.Content;
            existingblogPost.UrlHandle = editBlogPostRequest.UrlHandle;
            existingblogPost.PublishedDate = editBlogPostRequest.PublishedDate;
            existingblogPost.Author = editBlogPostRequest.Author;
            existingblogPost.FeaturedImageUrl = editBlogPostRequest.FeaturedImageUrl;
            existingblogPost.Visible = editBlogPostRequest.Visible;
            existingblogPost.ShortDescription = editBlogPostRequest.ShortDescription;

            var selectedTags = new List<Tag>();

            foreach (var tagId in editBlogPostRequest.SelectedTags)
            {
                var existingtag = await _tagRepository.GetTagAsync(Guid.Parse(tagId));

                if(existingtag != null)
                {
                    selectedTags.Add(existingtag);
                }
            }

            existingblogPost.Tags = selectedTags;

            var updatedblogpost = await _blogPostRepository.UpdateBlogPostAsync(existingblogPost);

            if (updatedblogpost != null)
            {
                //Show success notification
                return RedirectToAction("Edit", new { id = editBlogPostRequest.Id });
            }
            else
            {
                //Show failure notification
                return RedirectToAction("Edit", new { id =  editBlogPostRequest.Id });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditBlogPostRequest editBlogPostRequest)
        {
            var existingtag = await _blogPostRepository.DeleteBlogPostAsync(editBlogPostRequest.Id);

            if (existingtag != null)
            {
                //Show success Notification
                return RedirectToAction("List");
            }
            //Show failure notification
            return RedirectToAction("Edit", new { id =  editBlogPostRequest.Id });
        }
    }
}
