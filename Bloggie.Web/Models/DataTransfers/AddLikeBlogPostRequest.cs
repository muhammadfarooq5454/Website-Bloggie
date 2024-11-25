namespace Bloggie.Web.Models.DataTransfers
{
    public class AddLikeBlogPostRequest
    {
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
