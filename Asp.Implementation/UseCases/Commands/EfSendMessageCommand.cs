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
    public class EfSendMessageCommand : EfUseCase, ISendMessageCommand
    {
        private SendMessageValidator _validator;
        public EfSendMessageCommand(AspDbContext context, SendMessageValidator validator)
            : base(context)
        {
            _validator = validator;
        }
        public int Id => 8;

        public string Name => "Send message";

        public string Description => "Send message to your friend.";

        public void Execute(SendMessageDto request)
        {
            _validator.ValidateAndThrow(request);

            var message = new Message
            {
                FromUserId = request.UserId,
                ToUserId = request.ToId,
                Context = request.Context
            };

            Context.Messages.Add(message);
            Context.SaveChanges();

        }
    }
}
