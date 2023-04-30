using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OwlBlog.API.Contracts.Services.IServices;
using OwlBlog.API.Data.Models.Request.Comments;
using OwlBlog.API.Data.Models.Response.Comments;
using OwlBlog.API.Data.Models.Response.Users;
using OwlBlog.API.Data.Repositories.IRepositories;
using System.Net;

namespace OwlBlog.API.Contracts.Services
{
    public class CommentService : ICommentService
    {
        public IMapper _mapper;
        private ICommentRepository _commentRepo;
        private UserManager<User> _userManager;

        public CommentService(IMapper mapper, ICommentRepository commentRepo, UserManager<User> userManager)
        {
            _mapper = mapper;
            _commentRepo = commentRepo;
            _userManager = userManager;
        }

        public async Task<Guid> CreateComment(CommentCreateRequest model)
        {
            Comment comment = new Comment
            {
                Title = model.Title,
                Body = model.Description,
                Author = model.Author,
                PostId = model.PostId,
                AuthorId = Guid.Empty,
            };

            await _commentRepo.AddComment(comment);
            return comment.Id;
        }

        public async Task<int> EditComment(CommentEditRequest model)
        {
            var comment = _commentRepo.GetComment(model.Id);

            if (comment == null)
                return 0;

            comment.Title = model.Title;
            comment.Body = model.Description;
            comment.Author = model.Author;

            await _commentRepo.UpdateComment(comment);
            return 1;
        }

        public async Task RemoveComment(Guid id)
        {
            await _commentRepo.RemoveComment(id);
        }

        public async Task<List<Comment>> GetComments()
        {
            return _commentRepo.GetAllComments().ToList();
        }
    }
}
