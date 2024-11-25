using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Services
{
    public interface IBlogPostRepository
    {
        //Getting all BlogPosts
        Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync();

        //Getting a BlogPost
        Task<BlogPost?> GetBlogPostAsync(Guid id);

        //Adding A BlogPost
        Task<BlogPost> AddBlogPostAsync(BlogPost blogPost);

        //Updating A BlogPost
        Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost);

        //Deleting A BlogPost 
        Task<BlogPost?> DeleteBlogPostAsync(Guid id);

        //Get by Url Handle
        Task<BlogPost?> GetBlogPostByUrlHandleAsync(string urlHandle);

    }
}
 