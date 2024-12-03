namespace Bloggie.Web.Models.DataTransfers
{
    public class RemoveLikeBlogPostRequest
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
