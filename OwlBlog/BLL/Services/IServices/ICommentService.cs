using Microsoft.AspNetCore.Identity;
using OwlBlog.DAL.Models.Request.Comments;
using OwlBlog.DAL.Models.Request.Users;
using OwlBlog.DAL.Models.Response.Comments;
using OwlBlog.DAL.Models.Response.Users;

namespace OwlBlog.BLL.Services.IServices
{
    public interface ICommentService
    {
        Task<Guid> CreateComment(CommentCreateRequest model, Guid UserId);
        Task EditComment(CommentEditRequest model);
        Task RemoveComment(Guid id);
        Task<List<Comment>> GetComments();
    }
}
