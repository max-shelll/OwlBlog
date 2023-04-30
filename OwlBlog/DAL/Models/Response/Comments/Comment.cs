namespace OwlBlog.DAL.Models.Response.Comments
{
    public class Comment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }
        public string Body { get; set; }

        public string Author { get; set; }

        public Guid PostId { get; set; }
        public Guid AuthorId { get; set; }
        public string realAuthorName { get; set; }
    }
}
