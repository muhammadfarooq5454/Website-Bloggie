using Bloggie.Web.Models.Domain;

namespace Bloggie.Web.Services
{
    public interface IBlogPostLikeRepository
    {
        public Task<int> GetTotalLikes(Guid blogPostId);

        public Task<List<BlogPostLike>> GetAllLikesForBlogForUser(Guid blogPostId);

        public Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike);
        public Task<BlogPostLike> RemoveLikeForBlog(BlogPostLike blogPostLike);
    
    }
}
