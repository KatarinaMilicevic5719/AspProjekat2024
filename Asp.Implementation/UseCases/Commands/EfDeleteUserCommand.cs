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
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        public EfDeleteUserCommand(AspDbContext context) : base(context) { }
        public int Id => throw new NotImplementedException();

        public string Name => throw new NotImplementedException();

        public string Description => throw new NotImplementedException();

        public void Execute(DeleteDto request)
        {
            var user = Context.Users.Find(request.Id);

            if(user.Email != request.Email)
            {
                throw new UseCaseConflictException("You can delete only your own profile.");
            }

            Context.User = user.Email;

            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
