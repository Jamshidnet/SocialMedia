using SocialMedia.Domein.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialMedia.Domein.Entities
{
    public class Comment : BaseEntityAutibality
    {

        public Guid? PostId { get; set; }

        public Guid? BaseCommentId { get; set; }

        [Column("comment_text")]
        public string Text { get; set; }

        public Guid UserId { get; set; }



        [ForeignKey("UserId")]
        public User User { get; set; }
        
        
        [ForeignKey("BaseCommentId")]
        public Comment?  CommentObj { get; set; }
        
        [ForeignKey("PostId")]
        public Post? Post { get; set; }
        public ICollection<Comment>? Comments { get; set;}
    }
}
