using SocialMedia.Application.UseCases.Comments.Models;
using SocialMedia.Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.UseCases.Posts.Models
{
    public class PostDto
    {
        public Guid Id { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public ICollection<CommentDto> Comments { get; set; }

    }
}
