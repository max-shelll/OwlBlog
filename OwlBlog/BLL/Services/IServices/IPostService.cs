using OwlBlog.DAL.Models.Request.Comments;
using OwlBlog.DAL.Models.Request.Posts;
using OwlBlog.DAL.Models.Response.Posts;

namespace OwlBlog.BLL.Services.IServices
{
    public interface IPostService
    {
        Task<PostCreateRequest> CreatePost();
        Task<Guid> CreatePost(PostCreateRequest model);
        Task<PostEditViewModel> EditPost(Guid Id);
        Task EditPost(PostEditViewModel model, Guid Id);
        Task RemovePost(Guid id);
        Task<List<Post>> GetPosts();
        Task<Post> ShowPost(Guid id);
    }
}
