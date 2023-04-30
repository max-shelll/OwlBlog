using System.ComponentModel.DataAnnotations;

namespace OwlBlog.DAL.Models.Request.Comments
{
    public class CommentEditRequest
    {
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Описание")]
        public string Description { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Автор", Prompt = "Автор")]
        public string Author { get; set; }

        public Guid Id { get; set; }
    }
}
