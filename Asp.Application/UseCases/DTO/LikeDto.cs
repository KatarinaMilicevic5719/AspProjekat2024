using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Application.UseCases.DTO
{
    public class LikeDto
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
    }

    public class LikePost
    {
        public string UserLike { get; set; }
    }
}
