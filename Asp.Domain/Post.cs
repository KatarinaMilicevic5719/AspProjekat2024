using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Domain
{
    public class Post : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int? ImageId { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual Image Image { get; set; }
        public virtual ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
        public virtual ICollection<Like> PostLikes { get; set; } = new List<Like>();
        public virtual ICollection<Comment> PostComments { get; set; } = new List<Comment>();
    }
}
