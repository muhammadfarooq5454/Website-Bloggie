namespace Bloggie.Web.Models.DataTransfers
{
    public class EditTagRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }
}
