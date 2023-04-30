using OwlBlog.API.Data.Models.Request.Roles;
using OwlBlog.API.Data.Models.Response.Roles;
using System.ComponentModel.DataAnnotations;

namespace OwlBlog.API.Data.Models.Request.Users
{
    public class UserEditRequest
    {
        [Required]
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Имя")]
        public string? FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Фамилия")]
        public string? LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Никнейм", Prompt = "Никнейм")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Display(Name = "Почта")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string? NewPassword { get; set; }

    }
}
