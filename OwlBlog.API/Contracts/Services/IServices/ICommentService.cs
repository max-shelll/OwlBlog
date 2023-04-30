using Microsoft.AspNetCore.Mvc;
using OwlBlog.API.Data.Models.Request.Comments;
using OwlBlog.API.Data.Models.Response.Comments;

namespace OwlBlog.API.Contracts.Services.IServices
{
    public interface ICommentService
    {
        Task<Guid> CreateComment(CommentCreateRequest model);
        Task<int> EditComment(CommentEditRequest model);
        Task RemoveComment(Guid id);
        Task<List<Comment>> GetComments();
    }
}
