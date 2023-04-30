using OwlBlog.DAL.Models.Request.Roles;
using OwlBlog.DAL.Models.Request.Tags;
using OwlBlog.DAL.Models.Response.Roles;
using OwlBlog.DAL.Models.Response.Tags;

namespace OwlBlog.BLL.Services.IServices
{
    public interface IRoleService
    {
        Task<Guid> CreateRole(RoleCreateRequest model);
        Task EditRole(RoleEditRequest model);
        Task RemoveRole(Guid id);
        Task<List<Role>> GetRoles();
    }
}
