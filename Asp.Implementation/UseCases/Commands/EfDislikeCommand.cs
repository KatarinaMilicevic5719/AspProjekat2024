using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfDislikeCommand : EfUseCase, IDislikeCommand
    {
        public EfDislikeCommand(AspDbContext context) : base(context)
        {

        }
        public int Id => 11;

        public string Name => "Dislike the post";

        public string Description => "Dislike the post";

        public void Execute(LikeDto request)
        {
            var dislike = Context.Likes.Where(x => x.PostId == request.PostId &&
                                                   x.UserId == request.UserId &&
                                                   x.IsActive == true).FirstOrDefault();

            if(dislike == null)
            {
                throw new EntityNotFoundException();
            }

            var user = Context.Users.Find(request.UserId);

            Context.User = user.Email;
            Context.Likes.Remove(dislike);
            Context.SaveChanges();
        }

    }
}
