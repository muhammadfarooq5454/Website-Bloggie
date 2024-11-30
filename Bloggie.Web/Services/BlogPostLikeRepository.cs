
using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Services
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostLikeRepository(BloggieDbContext bloggieDbContext)
        {
            this.bloggieDbContext = bloggieDbContext;
        }

        public async Task<BlogPostLike> AddLikeForBlog(BlogPostLike blogPostLike)
        {
            await bloggieDbContext.blogPostLike.AddAsync(blogPostLike);
            await bloggieDbContext.SaveChangesAsync();
            return blogPostLike;
        }

        public async Task<List<BlogPostLike>> GetAllLikesForBlogForUser(Guid blogPostId)
        {
            return await bloggieDbContext.blogPostLike.Where(x => x.BlogPostId == blogPostId).ToListAsync();
        }

        public async Task<int> GetTotalLikes(Guid blogPostId)
        {   
            int totalcount = await bloggieDbContext.blogPostLike.CountAsync(x => x.BlogPostId == blogPostId);
            return totalcount;
        }
    }
}
