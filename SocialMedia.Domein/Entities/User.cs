using SocialMedia.Domein.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Domein.Entities
{
    public  class User : BaseEntityAutibality
    {
        public string UserName { get; set; }

        public int Age { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
