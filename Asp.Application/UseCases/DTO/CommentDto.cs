using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Application.UseCases.DTO
{
    public class CommentDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Comment { get; set; }
        public int? ParentId { get; set; }
    }

    public class EditCommentDto : CommentDto
    {
        public int CommentId { get; set; }
    }

    public class GetComment
    {
        public string Context { get; set; }
        public string User { get; set; }
        public ICollection<ChildComment> Children { get; set; }
    }

    public class ChildComment : GetComment
    {

    }
}
