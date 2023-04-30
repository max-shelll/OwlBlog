using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using OwlBlog.BLL.Services.IServices;
using OwlBlog.DAL.Models.Request.Comments;
using OwlBlog.DAL.Models.Request.Posts;
using OwlBlog.DAL.Models.Request.Tags;
using OwlBlog.DAL.Models.Response.Comments;
using OwlBlog.DAL.Models.Response.Posts;
using OwlBlog.DAL.Models.Response.Tags;
using OwlBlog.DAL.Models.Response.Users;
using OwlBlog.DAL.Repositories;
using OwlBlog.DAL.Repositories.IRepositories;
using System.Linq;
using System.Security.Claims;

namespace OwlBlog.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _repo;
        private readonly ITagRepository _tagRepo;
        private readonly UserManager<User> _userManager;
        private readonly ICommentRepository _commentRepo;
        private IMapper _mapper;

        public PostService(ITagRepository tagRepo, IPostRepository repo, IMapper mapper, UserManager<User> userManager, ICommentRepository commentRepo)
        {
            _repo = repo;
            _tagRepo = tagRepo;
            _mapper = mapper;
            _userManager = userManager;
            _commentRepo = commentRepo;
        }

        public async Task<PostCreateRequest> CreatePost()
        {
            Post post = new Post();

            var allTags = _tagRepo.GetAllTags().Select(t => new TagRequest() { Id = t.Id, Name = t.Name }).ToList();

            PostCreateRequest model = new PostCreateRequest
            {
                Title = post.Title = string.Empty,
                Body = post.Body = string.Empty,
                Tags = allTags
            };

            return model;
        }

        public async Task<Guid> CreatePost(PostCreateRequest model)
        {
            var dbTags = new List<Tag>();

            if (model.Tags != null)
            {
                var postTags = model.Tags.Where(t => t.IsSelected == true).ToList();
                var tagsId = postTags.Select(t => t.Id).ToList();
                dbTags = _tagRepo.GetAllTags().Where(t => tagsId.Contains(t.Id)).ToList();
            }

            Post post = new Post
            {
                Id = model.Id,
                Title = model.Title,
                Body = model.Body,
                Tags = dbTags,
                AuthorId = model.AuthorId
            };

            var user = await _userManager.FindByIdAsync(model.AuthorId);
            user.Posts.Add(post);

            await _repo.AddPost(post);
            await _userManager.UpdateAsync(user);

            return post.Id;
        }

        public async Task<PostEditViewModel> EditPost(Guid id)
        {
            var post = _repo.GetPost(id);

            var tags = _tagRepo.GetAllTags().Select(t => new TagRequest() { Id = t.Id, Name = t.Name }).ToList();

            foreach (var tag in tags)
            {
                foreach (var postTag in post.Tags)
                {
                    if (postTag.Id == tag.Id)
                    {
                        tag.IsSelected = true;
                        break;
                    }
                }
            }

            var model = new PostEditViewModel()
            {
                id = id,
                Title = post.Title,
                Body = post.Body,
                Tags = tags
            };

            return model;
        }

        public async Task EditPost(PostEditViewModel model, Guid Id)
        {
            var post = _repo.GetPost(Id);

            post.Title = model.Title;
            post.Body = model.Body;

            foreach (var tag in model.Tags)
            {
                var tagChanged = _tagRepo.GetTag(tag.Id);
                if (tag.IsSelected)
                {
                    post.Tags.Add(tagChanged);
                }
                else
                {
                    post.Tags.Remove(tagChanged);
                }
            }

            await _repo.UpdatePost(post);
        }

        public async Task RemovePost(Guid id)
        {
            await _repo.RemovePost(id);
        }

        public async Task<List<Post>> GetPosts()
        {
            var posts = _repo.GetAllPosts().ToList();

            return posts;
        }

        public async Task<Post> ShowPost(Guid id)
        {
            var post = _repo.GetPost(id);
            var user = await _userManager.FindByIdAsync(post.AuthorId.ToString());

            var comments = _commentRepo.GetCommentsByPostId(post.Id);
            post.Id = id;

            foreach (var comment in comments)
            {
                if (post.Comments.FirstOrDefault(c => c.Id == comment.Id) == null)
                {
                    post.Comments.Add(comment);
                }
            }

            if (!string.IsNullOrEmpty(user.UserName))
            {
                post.AuthorId = user.UserName;
            }
            else
            {
                post.AuthorId = "nonUsernamed";
            }

            return post;
        }
    }
}
