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
    public class EfDisconnectFriendsCommand : EfUseCase, IDeleteCommand
    {
        public EfDisconnectFriendsCommand(AspDbContext context) : base(context)
        {

        }
        public int Id => 16;

        public string Name => "Unfollow friend";

        public string Description => "Unfollow friend";

        public void Execute(DeleteDto request)
        {
            var user = Context.Users.Where(x => x.Email == request.Email)
                                    .Select(x => x.Id).FirstOrDefault();

            var friend = Context.UserFriends.Find(request.Id);

            if(friend == null || friend.IsActive == false)
            {
                throw new EntityNotFoundException();
            }

            if(friend.RequestUserId != user && friend.AcceptUserId != user)
            {
                throw new UseCaseConflictException("You aren't following this user.");
            }

            Context.User = request.Email;
            Context.UserFriends.Remove(friend);
            Context.SaveChanges();
        }
    }
}
