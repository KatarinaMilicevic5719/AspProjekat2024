using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Domain
{
    public class PostCategory : Entity
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }
        public virtual Post Post { get; set; }
        public virtual Category Category { get; set; }
    }
}
