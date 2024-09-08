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
    public class EfDeleteMessageCommand : EfUseCase, IDeleteMessageCommand
    {
        public EfDeleteMessageCommand(AspDbContext context) : base(context) { }
        public int Id => 20;

        public string Name => "Delete message";

        public string Description => "Delete message";

        public void Execute(DeleteDto request)
        {
            var message = Context.Messages.Find(request.Id);

            if(message == null || message.IsActive == false)
            {
                throw new EntityNotFoundException();
            }

            var user = Context.Users.Where(x => x.Email == request.Email).FirstOrDefault();

            if(user.Id != message.FromUserId)
            {
                throw new UseCaseConflictException("You can delete only message that you sent.");
            }

            Context.User = request.Email;
            Context.Messages.Remove(message);
            Context.SaveChanges();
        }
    }
}
