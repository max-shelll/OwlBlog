using System.ComponentModel.DataAnnotations;
using OwlBlog.API.Data.Models.Request.Tags;
using OwlBlog.API.Data.Models.Response.Tags;
using OwlBlog.API.Data.Repositories;
using OwlBlog.API.Data.Repositories.IRepositories;

namespace OwlBlog.API.Data.Models.Request.Posts
{
    public class PostCreateRequest
    {
        public string AuthorId { get; set; }
        public List<TagRequest> Tags { get; set; }


        [Required(ErrorMessage = "Поле Заголовок обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле Описание обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Описание")]
        public string Body { get; set; }
    }
}
