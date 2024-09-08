using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using Asp.Implementation.Validation;
using FluentValidation;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfEditCommentCommand : EfUseCase, IEditCommentCommand
    {
        private EditCommentValidator _validator;
        public EfEditCommentCommand(AspDbContext context, EditCommentValidator validator)
            : base(context)
        {
            _validator = validator;
        }
        public int Id => 14;

        public string Name => "Edit comment";

        public string Description => "Edit comment";

        public void Execute(EditCommentDto request)
        {
            _validator.ValidateAndThrow(request);

            var comment = Context.Comments.Find(request.CommentId);

            if(comment == null || comment.IsActive == false)
            {
                throw new EntityNotFoundException();
            }

            if(comment.UserId != request.UserId)
            {
                throw new UseCaseConflictException("You can edit only comments that you write.");
            }

            comment.PostId = request.PostId;
            comment.UserId = request.UserId;
            comment.ParentId = request.ParentId;
            comment.Context = request.Comment;

            var user = Context.Users.Find(request.UserId);
            Context.User = user.Email;
            Context.Comments.Update(comment);
            Context.SaveChanges();
            
        }
    }
}
