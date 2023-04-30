using AutoMapper;
using OwlBlog.DAL.Models.Request.Comments;
using OwlBlog.DAL.Models.Request.Posts;
using OwlBlog.DAL.Models.Request.Tags;
using OwlBlog.DAL.Models.Request.Users;
using OwlBlog.DAL.Models.Response.Comments;
using OwlBlog.DAL.Models.Response.Posts;
using OwlBlog.DAL.Models.Response.Tags;
using OwlBlog.DAL.Models.Response.Users;

namespace OwlBlog.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(x => x.Email, opt => opt.MapFrom(c => c.Email))
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.UserName));

            CreateMap<CommentCreateRequest, Comment>();
            CreateMap<CommentEditRequest, Comment>();
            CreateMap<PostCreateRequest, Post>();
            CreateMap<PostEditViewModel, Post>();
            CreateMap<TagCreateRequest, Tag>();
            CreateMap<TagEditRequest, Tag>();
            CreateMap<UserEditRequest, User>();
        }
    }
}
