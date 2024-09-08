using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfDeletePostCommand : EfUseCase, IDeletePostCommand
    {
        public EfDeletePostCommand(AspDbContext context) : base(context) { }
        public int Id => 22;

        public string Name => "Delete post";

        public string Description => "Delete post";

        public void Execute(DeleteDto request)
        {
            var post = Context.Posts.Include(x => x.User)
                                    .FirstOrDefault(x => x.Id == request.Id);

            if(post == null)
            {
                throw new EntityNotFoundException();
            }

            var user = Context.Users.Where(x => x.Email == request.Email).FirstOrDefault();

            if(post.UserId != user.Id)
            {
                throw new UseCaseConflictException("You can delete only your posts.");
            }

            Context.User = request.Email;
            Context.Posts.Remove(post);
            Context.SaveChanges();
        }
    }
}
