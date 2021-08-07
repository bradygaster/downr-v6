using System;

namespace downr
{
    public class Post
    {
        public string Slug { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime LastModified { get; set; }
        public string Author { get; set; }
        public string[] Categories { get; set; } = new string[0];
        public string Content { get; set; }
        public string Description { get; set; }
    }
}