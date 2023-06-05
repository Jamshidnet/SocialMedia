using SocialMedia.Application.UseCases.Posts.Models;
using SocialMedia.Domein.Entities;

namespace SocialMedia.Application.UseCases.Users.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public int Age { get; set; }

        public ICollection<PostDto> Posts { get; set; }
    }
}
