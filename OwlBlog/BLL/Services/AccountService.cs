using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OwlBlog.BLL.Services.IServices;
using OwlBlog.DAL.Models.Request.Roles;
using OwlBlog.DAL.Models.Request.Users;
using OwlBlog.DAL.Models.Response.Roles;
using OwlBlog.DAL.Models.Response.Tags;
using OwlBlog.DAL.Models.Response.Users;
using OwlBlog.DAL.Repositories;
using OwlBlog.DAL.Repositories.IRepositories;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace OwlBlog.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IPostRepository _postRepo;
        public IMapper _mapper;

        public AccountService(IPostRepository postRepo, RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _postRepo = postRepo;
        }

        public async Task<IdentityResult> Register(RegisterRequest model)
        {
            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);

                var userRole = new Role() { Name = "Пользователь", SecurityLvl = 0 };
                await _roleManager.CreateAsync(userRole);

                var currentUser = await _userManager.FindByIdAsync(Convert.ToString(user.Id));
                await _userManager.AddToRoleAsync(currentUser, userRole.Name);

                return result;
            }
            else
            {
                return result;
            }
        }

        public async Task<SignInResult> Login(LoginRequest model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            return result;
        }

        public async Task<UserEditRequest> EditAccount(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var allRolesName = _roleManager.Roles.ToList();

            UserEditRequest model = new UserEditRequest
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                NewPassword = string.Empty,
                Id = id,
                Roles = allRolesName.Select(r => new RoleRequest() { Id = new Guid(r.Id), Name = r.Name }).ToList(),
            };

            return model;
        }

        public async Task<IdentityResult> EditAccount(UserEditRequest model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());

            if (model.FirstName != null)
            {
                user.FirstName = model.FirstName;
            }
            if (model.LastName != null)
            {
                user.LastName = model.LastName;
            }
            if (model.Email != null)
            {
                user.Email = model.Email;
            }
            if (model.NewPassword != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
            }
            if (model.UserName != null)
            {
                user.UserName = model.UserName;
            }

           foreach (var role in model.Roles)
           {
                var roleName = _roleManager.FindByIdAsync(role.Id.ToString()).Result.Name;

                if (role.IsSelected)
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
           }

            var result = await _userManager.UpdateAsync(user);
            return result;
        }

        public async Task RemoveAccount(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            await _userManager.DeleteAsync(user);
        }

        public async Task<List<User>> GetAccounts()
        {
            var accounts = _userManager.Users.Include(u => u.Posts).ToList();

            foreach (var user in accounts)
            {
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    var newRole = new Role { Name = role };
                    user.Roles.Add(newRole);
                }
            }

            return accounts;
        }

        public async Task LogoutAccount()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
