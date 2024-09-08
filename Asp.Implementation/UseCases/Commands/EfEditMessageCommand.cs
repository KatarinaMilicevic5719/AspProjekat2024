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
    public class EfEditMessageCommand : EfUseCase, IEditMessageCommand
    {
       
        public EfEditMessageCommand(AspDbContext context): base(context) { }
        public int Id => 19;

        public string Name => "Update message";

        public string Description => "Update message";

        public void Execute(EditMessageDto request)
        {
            var message = Context.Messages.Find(request.MessageId);

            if(message == null || message.IsActive == false)
            {
                throw new EntityNotFoundException();
            }

            if(message.FromUserId != request.UserId)
            {
                throw new UseCaseConflictException("You can edit only message that you send.");
            }

            if (!string.IsNullOrEmpty(request.Context))
            {
                message.Context = request.Context;
            }

            message.Seen = request.Seen;

            var user = Context.Users.Find(request.UserId);
            Context.User = user.Email;

            Context.Messages.Update(message);
            Context.SaveChanges();
        }
    }
}
