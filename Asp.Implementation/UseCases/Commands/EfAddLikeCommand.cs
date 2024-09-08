using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using Asp.Domain;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfAddLikeCommand : EfUseCase, IAddLikeCommand
    {
        public EfAddLikeCommand(AspDbContext context) : base(context)
        {

        }
        public int Id => 10;

        public string Name => "Like a post";

        public string Description => "Like a post";

        public void Execute(LikeDto request)
        {
            var post = Context.Posts.Find(request.PostId);

            if(post == null)
            {
                throw new EntityNotFoundException();
            }

            var like = new Like
            {
                UserId = request.UserId,
                PostId = request.PostId
            };

            Context.Likes.Add(like);
            Context.SaveChanges();
        }
    }
}
