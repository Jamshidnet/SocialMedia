using SocialMedia.Domein.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Application.UseCases.Comments.Models
{
    public  class CommentDto
    {
        public Guid Id { get; set; }

        public Guid? PostId { get; set; }
        public string  Text { get; set; }

        public Guid? BaseCommentId { get; set; }

        public ICollection<CommentDto>? Comments { get; set; }
    }
}
 