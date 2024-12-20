﻿namespace Bloggie.Web.Models.Domain
{
    public class BlogPost
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeaturedImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        //Navigation Properties
        public ICollection<Tag> Tags { get; set; } //ICollection (Many to Many Relationship)

        //Navigation Property 
        public ICollection<BlogPostLike> Likes { get; set; } // (1 to Many Relationship)


    }
}
