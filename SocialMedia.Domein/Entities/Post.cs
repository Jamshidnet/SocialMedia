using SocialMedia.Domein.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domein.Entities
{
    public class Post : BaseEntityAutibality
    {
        public string Text { get; set; }

        public Guid UserId { get; set; }
        public ICollection<Comment> Comments { get; set; }


        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
