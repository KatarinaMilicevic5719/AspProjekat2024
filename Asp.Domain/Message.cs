using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Domain
{
    public class Message : Entity
    {
        public string Context { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public bool Seen { get; set; } 

        public virtual User FromUser { get; set; }
        public virtual User ToUser { get; set; }

    }
}
