using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Domain
{
    public class User : Entity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Post> UserPosts { get; set; } = new List<Post>();
        public virtual ICollection<Like> UserLikes { get; set; } = new List<Like>();
        public virtual ICollection<UserFriends> RequestedFriends { get; set; } = new List<UserFriends>();
        public virtual ICollection<UserFriends> AcceptedFriends { get; set; } = new List<UserFriends>();
        public virtual ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public virtual ICollection<Message> RecivedMessages { get; set; } = new List<Message>();
        public virtual ICollection<UserUseCase> UseCases { get; set; }
    }
}
