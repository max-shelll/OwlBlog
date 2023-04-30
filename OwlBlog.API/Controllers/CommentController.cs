using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlBlog.API.Contracts.Services.IServices;
using OwlBlog.API.Data.Models.Request.Comments;
using OwlBlog.API.Data.Models.Request.Users;
using OwlBlog.API.Data.Models.Response.Comments;

namespace OwlBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех комментарий поста
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("GetPostComment")]
        public async Task<IEnumerable<Comment>> GetUsers(Guid id)
        {
            var comment = await _commentService.GetComments();
            return comment.Where(c => c.PostId == id);
        }

        /// <summary>
        /// Создания комментария к посту
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        [Route("CreateComment")]
        public async Task<IActionResult> CreateComment(CommentCreateRequest request)
        {
            var result = await _commentService.CreateComment(request);
            return StatusCode(201);
        }

        /// <summary>
        /// Редактирование комментария
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPatch]
        [Route("EditComment")]
        public async Task<IActionResult> EditComment(CommentEditRequest request)
        {
            var result = await _commentService.EditComment(request);
            if (result == 1)
                return StatusCode(201);
            else
                return StatusCode(204);
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpDelete]
        [Route("RemoveComment")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);

            return StatusCode(201);
        }
    }
}
