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
    public class EfConnectFriendsCommand : EfUseCase, IConnectFriendsCommand
    {
        private ConnectFriendsValidator _validator;

        public EfConnectFriendsCommand(AspDbContext context, ConnectFriendsValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 3;

        public string Name => "Connect friends";

        public string Description => "Connect friends upon their request.";

        public void Execute(FriendsDto request)
        {
            _validator.ValidateAndThrow(request);

            var friends = new UserFriends
            {
                RequestUserId = request.RequestUserId.GetValueOrDefault(),
                AcceptUserId = request.AcceptUserId.GetValueOrDefault()
            };

            Context.UserFriends.Add(friends);
            Context.SaveChanges();
        }
    }
}
