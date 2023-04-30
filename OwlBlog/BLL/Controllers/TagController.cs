using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlBlog.BLL.Services.IServices;
using OwlBlog.DAL.Models.Request.Tags;
using OwlBlog.DAL.Models.Response.Tags;
using OwlBlog.DAL.Repositories.IRepositories;

namespace OwlBlog.BLL.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _repo;
        private readonly ITagService _tagService;
        private readonly ILogger<TagController> _logger;
        private IMapper _mapper;
        public TagController(ITagRepository repo, IMapper mapper, ITagService tagService, ILogger<TagController> logger)
        {
            _repo = repo;
            _mapper = mapper;
            _tagService = tagService;
            _logger = logger;
        }

        /// <summary>
        /// [Get] Метод, создания тега
        /// </summary>
        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            return View();
        }

        /// <summary>
        /// [Post] Метод, создания тега
        /// </summary>
        [Route("Tag/Create")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> CreateTag(TagCreateRequest model)
        {
            if (ModelState.IsValid)
            {
                var tagId = _tagService.CreateTag(model);
                _logger.LogInformation($"Создан тег - {model.Name}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, редактирования тега
        /// </summary>
        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult EditTag(Guid id)
        {
            var view = new TagEditRequest { Id = id};
            return View(view);
        }

        /// <summary>
        /// [Post] Метод, редактирования тега
        /// </summary>
        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditTag(TagEditRequest model)
        {
            if (ModelState.IsValid)
            {
                await _tagService.EditTag(model);
                _logger.LogDebug($"Изменен тег - {model.Name}");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] Метод, удаления тега
        /// </summary>
        [Route("Tag/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveTag(Guid id, bool isConfirm = true)
        {
            if (isConfirm)
                await RemoveTag(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] Метод, удаления тега
        /// </summary>
        [Route("Tag/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            await _tagService.RemoveTag(id);
            _logger.LogDebug($"Удаленн тег - {id}");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] Метод, получения всех тегов
        /// </summary>
        [Route("Tag/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.GetTags();
            return View(tags);
        }
    }
}
