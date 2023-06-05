using AutoMapper;
using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Application.UseCases.Users.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.Mapping;

public class MappingProfile: Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Post, PostDto>().ReverseMap();
        CreateMap<Comment,CommentDto>().ReverseMap();
    }

}
