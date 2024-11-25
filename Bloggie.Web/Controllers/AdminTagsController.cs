using Bloggie.Web.Data;
using Bloggie.Web.Models.DataTransfers;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagsController(ITagRepository _tagRepository)
        {
            tagRepository = _tagRepository;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            //Model Binding
            var tag = new Tag
            {
                Name = addTagRequest.Name,
                DisplayName = addTagRequest.DisplayName
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tags = await tagRepository.GetAllTagsAsync();

            return View(tags);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        //Reading Route-id
        public async Task<IActionResult> Edit(Guid id) 
        {
            var existingtag = await tagRepository.GetTagAsync(id);

            if(existingtag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = existingtag.Id,
                    Name = existingtag.Name,
                    DisplayName = existingtag.DisplayName
                };
                return View(editTagRequest);
            }
            return View(null);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag(); 

            tag.Id = editTagRequest.Id;
            tag.Name = editTagRequest.Name;
            tag.DisplayName = editTagRequest.DisplayName;
     

            var updatedtag = await tagRepository.UpdateAsync(tag);

            if(updatedtag != null)
            {
                //Show success notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }
            else
            {
                //Show failure notification
                return RedirectToAction("Edit", new { id = editTagRequest.Id});
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var existingtag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if(existingtag != null)
            {
                //Show success Notification
                return RedirectToAction("List");
            }
            //Show failure notification
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        /*[HttpGet] //My Logic Function
        public IActionResult Delete(Guid id)
        {

            var tag = bloggieDbContext.Tags.Find(id);

            if(tag != null)
            {
                bloggieDbContext.Tags.Remove(tag);
                bloggieDbContext.SaveChanges(); 
            }
            return RedirectToAction("List");
        }*/
    }
}
