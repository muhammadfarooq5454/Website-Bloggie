using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Services
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public BlogPostRepository(BloggieDbContext _bloggieDbContext)
        {
            bloggieDbContext = _bloggieDbContext;
        }

        public async Task<BlogPost> AddBlogPostAsync(BlogPost blogPost)
        {
            await bloggieDbContext.AddAsync(blogPost);
            await bloggieDbContext.SaveChangesAsync();

            return blogPost;
        }

        public async Task<BlogPost?> DeleteBlogPostAsync(Guid id)
        {
            var existingblogPost = await bloggieDbContext.BlogPosts.FindAsync(id);

            if (existingblogPost != null)
            {
                bloggieDbContext.BlogPosts.Remove(existingblogPost);
                await bloggieDbContext.SaveChangesAsync();

                return existingblogPost;
            }
            else
            {
                return null;
            }
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPostsAsync()
        {
            return await bloggieDbContext.BlogPosts.Include(b => b.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetBlogPostAsync(Guid id)
        {
            return await bloggieDbContext.BlogPosts.Include(b => b.Tags).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlogPost?> GetBlogPostByUrlHandleAsync(string urlHandle)
        {
           return await bloggieDbContext.BlogPosts.Include(p => p.Tags).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateBlogPostAsync(BlogPost blogPost)
        {
            var existingblogPost = await bloggieDbContext.BlogPosts.Include(b => b.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);

            if (existingblogPost != null)
            {
                existingblogPost.Heading = blogPost.Heading;
                existingblogPost.PageTitle = blogPost.PageTitle;
                existingblogPost.Content = blogPost.Content;
                existingblogPost.UrlHandle = blogPost.UrlHandle;
                existingblogPost.PublishedDate = blogPost.PublishedDate;
                existingblogPost.Author = blogPost.Author;
                existingblogPost.ShortDescription = blogPost.ShortDescription;
                existingblogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingblogPost.Visible = blogPost.Visible;
                existingblogPost.Tags = blogPost.Tags;

                await bloggieDbContext.SaveChangesAsync();

                return existingblogPost;
            }
            else
            {
                return null;
            }
        }
    }
}
