using Bloggie.Web.Models.DataTransfers;
using Bloggie.Web.Models.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Services
{
    public interface ITagRepository
    {
        //Getting All Tags
        Task<IEnumerable<Tag>> GetAllTagsAsync();

        //Getting A Tag
        Task<Tag?> GetTagAsync(Guid id);

        //Adding A Tag
        Task<Tag> AddAsync(Tag tag);

        //Edit A Tag
        Task<Tag?> UpdateAsync(Tag tag);

        //Delete A Tag
        Task<Tag?> DeleteAsync(Guid id);

    }
}
