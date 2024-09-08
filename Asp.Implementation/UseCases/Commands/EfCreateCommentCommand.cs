using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using Asp.Domain;
using Asp.Implementation.Validation;
using FluentValidation;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfCreateCommentCommand : EfUseCase, ICreateCommentCommand
    {
        private CommentValidator _validator;
        public EfCreateCommentCommand(AspDbContext context, CommentValidator validator)
            : base(context)
        {
            _validator = validator;
        }
        public int Id => 13;

        public string Name => "Create a comment";

        public string Description => "Create a comment";

        public void Execute(CommentDto request)
        {
            _validator.ValidateAndThrow(request);

            var comment = new Comment
            {
                PostId = request.PostId,
                UserId = request.UserId,
                Context = request.Comment,
                ParentId = request.ParentId
            };

            Context.Comments.Add(comment);
            Context.SaveChanges();
            
        }
    }
}
