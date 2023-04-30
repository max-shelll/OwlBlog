using System.ComponentModel.DataAnnotations;

namespace OwlBlog.API.Data.Models.Request.Comments
{
    public class CommentCreateRequest
    {
        [Required(ErrorMessage = "Поле Заголовок обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле Описание обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Поле Автор обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Автор", Prompt = "Автор")]
        public string Author { get; set; }

        [Required]
        public Guid PostId { get; set; }
    }
}
