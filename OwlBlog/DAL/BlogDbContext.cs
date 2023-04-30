using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OwlBlog.DAL.Models.Response.Comments;
using OwlBlog.DAL.Models.Response.Posts;
using OwlBlog.DAL.Models.Response.Roles;
using OwlBlog.DAL.Models.Response.Tags;
using OwlBlog.DAL.Models.Response.Users;
using System.Xml;

namespace OwlBlog.DAL
{
    public class BlogDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }


        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

