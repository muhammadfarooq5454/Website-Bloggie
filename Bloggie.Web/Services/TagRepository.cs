using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Services
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext bloggieDbContext;

        public TagRepository(BloggieDbContext _bloggieDbContext)
        {
            bloggieDbContext = _bloggieDbContext;        
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await bloggieDbContext.Tags.AddAsync(tag);
            await bloggieDbContext.SaveChangesAsync();

            return tag;

        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            var alltags = await bloggieDbContext.Tags.ToListAsync();
            return alltags;
        }

        public async Task<Tag?> GetTagAsync(Guid id)
        {
            return await bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(tag.Id);

            if(existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }
            else
            {
                return null;
            }
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var existingTag = await bloggieDbContext.Tags.FindAsync(id);

            if (existingTag != null)
            {
                bloggieDbContext.Tags.Remove(existingTag);
                await bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }
            else
            {
                return null;
            }

        }
    }
}
