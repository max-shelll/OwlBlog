using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using OwlBlog.DAL.Models.Request.Users;
using OwlBlog.DAL.Models.Response.Users;

namespace OwlBlog.BLL.Services.IServices
{
    public interface IAccountService
    {
        Task<IdentityResult> Register(RegisterRequest model);
        Task<SignInResult> Login (LoginRequest model);
        Task<IdentityResult> EditAccount(UserEditRequest model);
        Task<UserEditRequest> EditAccount(Guid id);
        Task RemoveAccount(Guid id);
        Task<List<User>> GetAccounts();
        Task LogoutAccount();
    }
}
