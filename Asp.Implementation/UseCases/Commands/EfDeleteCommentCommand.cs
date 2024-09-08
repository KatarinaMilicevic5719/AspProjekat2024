using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.DataAccess;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfDeleteCommentCommand : EfUseCase, IDeleteCommand
    {
        public EfDeleteCommentCommand(AspDbContext context) : base(context)
        {

        }
        public int Id => 15;

        public string Name => "Delete comment";

        public string Description => "Delete comment";

        public void Execute(DeleteDto request)
        {
            var comment = Context.Comments.Find(request.Id);

            if(comment == null || comment.IsActive == false)
            {
                throw new EntityNotFoundException();
            }

            var user = Context.Users.Find(comment.UserId);
            if(user.Email != request.Email)
            {
                throw new UseCaseConflictException("You can delete only yours comment.");
            }

            Context.User = request.Email;
            Context.Comments.Remove(comment);
            Context.SaveChanges();
        }
    }
}
