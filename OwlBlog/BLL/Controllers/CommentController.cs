using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OwlBlog.BLL.Services;
using OwlBlog.BLL.Services.IServices;
using OwlBlog.DAL.Models.Request.Comments;
using OwlBlog.DAL.Models.Request.Tags;
using OwlBlog.DAL.Models.Response.Comments;
using OwlBlog.DAL.Models.Response.Users;
using OwlBlog.DAL.Repositories.IRepositories;

namespace OwlBlog.BLL.Controllers
{
    public class CommentController : Controller
    {
        private IMapper _mapper;
        private ICommentRepository _commentRepo;
        private ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public CommentController(IMapper mapper, ICommentRepository commentRepo, ICommentService commentService, UserManager<User> userManager)
        {
            _mapper = mapper;
            _commentRepo = commentRepo;
            _commentService = commentService;
            _userManager = userManager;
        }

        // <summary>
        /// [Get] Метод, добавление комментария
        /// </summary>
        [HttpGet]
        [Route("Comment/CreateComment")]
        public IActionResult CreateComment(Guid postId)
        {
            var model = new CommentCreateRequest() { PostId = postId };
            return View(model);
        }

        // <summary>
        /// [Post] Метод, добавление комментария
        /// </summary>
        [HttpPost]
        [Route("Comment/CreateComment")]
        public async Task<IActionResult> CreateComment(CommentCreateRequest model, Guid PostId)
        {
            model.PostId = PostId;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = _commentService.CreateComment(model, new Guid(user.Id));
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, редактирования коментария
        /// </summary>
        [Route("Comment/Edit")]
        [HttpGet]
        public IActionResult EditComment(Guid id)
        {
            var view = new CommentEditRequest { Id = id };
            return View(view);
        }

        /// <summary>
        /// [Post] Метод, редактирования коментария
        /// </summary>
        [Authorize]
        [Route("Comment/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentEditRequest model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditComment(model);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, удаления коментария
        /// </summary>
        [HttpGet]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveComment(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Delete] Метод, удаления коментария
        /// </summary>
        [HttpDelete]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);
            return RedirectToAction("Index", "Home");
        }
    }
}
