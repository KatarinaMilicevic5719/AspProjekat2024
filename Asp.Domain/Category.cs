using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Domain
{
    public class Category : Entity
    {
        public string CategoryName { get; set; }
        public virtual ICollection<PostCategory> PostsCategory { get; set; } = new List<PostCategory>();
    }
}
