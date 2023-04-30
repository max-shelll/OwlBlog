using Microsoft.AspNetCore.Identity;
using OwlBlog.API.Data.Models.Response.Posts;
using OwlBlog.API.Data.Models.Response.Roles;

namespace OwlBlog.API.Data.Models.Response.Users
{
    public class User : IdentityUser
    {
        //Id, FirstName, LastName, UserName, Email -> распологаются в классе родителя

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime CreatedData { get; set; } = DateTime.Now;

        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
