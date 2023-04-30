using OwlBlog.DAL.Models.Request.Tags;
using OwlBlog.DAL.Models.Response.Tags;

namespace OwlBlog.BLL.Services.IServices
{
    public interface ITagService
    {
        Task<Guid> CreateTag(TagCreateRequest model);
        Task EditTag(TagEditRequest model);
        Task RemoveTag(Guid id);
        Task<List<Tag>> GetTags();
    }
}
