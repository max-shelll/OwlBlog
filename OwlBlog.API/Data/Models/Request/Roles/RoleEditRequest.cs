using System.ComponentModel.DataAnnotations;

namespace OwlBlog.API.Data.Models.Request.Roles
{
    public class RoleEditRequest
    {
        public Guid Id { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Уровень доступа", Prompt = "Уровень")]
        public int? SecurityLvl { get; set; } = null;
    }
}
