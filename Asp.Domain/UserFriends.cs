using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Domain
{
    public class UserFriends : Entity
    {
        public int RequestUserId { get; set; }
        public int AcceptUserId { get; set; }

        public virtual User RequestUser { get; set; }
        public virtual User AcceptUser { get; set; }
    }
}
