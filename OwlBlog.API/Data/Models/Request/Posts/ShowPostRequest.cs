using OwlBlog.API.Data.Models.Request.Tags;

namespace OwlBlog.API.Data.Models.Request.Posts
{
    public class ShowPostRequest
    {
        public Guid Id { get; set; }

        public string AuthorId { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
