using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlBlog.API.Contracts.Services.IServices;
using OwlBlog.API.Data.Models.Request.Posts;
using OwlBlog.API.Data.Models.Request.Users;

namespace OwlBlog.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех постов
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IEnumerable<ShowPostRequest>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            var postsResponse = posts.Select(p => new ShowPostRequest { Id = p.Id, AuthorId = p.AuthorId, Title = p.Title, Body = p.Body, Tags = p.Tags.Select(_ => _.Name)}).ToList();
            return postsResponse;
        }

        /// <summary>
        /// Добавление поста
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(PostCreateRequest request)
        {
            var result = await _postService.CreatePost(request);
            return StatusCode(201);
        }

        /// <summary>
        /// Редактирование поста
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPatch]
        [Route("EditPost")]
        public async Task<IActionResult> EditPost(PostEditRequest request)
        {
            await _postService.EditPost(request, request.id);

            return StatusCode(201);
        }

        /// <summary>
        /// Удаление поста
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpDelete]
        [Route("RemovePost")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await _postService.RemovePost(id);

            return StatusCode(201);
        }
    }
}
