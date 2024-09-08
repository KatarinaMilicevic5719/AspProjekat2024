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
    public class EfModiefiedUserUseCaseCommand : EfUseCase, IModifiedUserUseCasesCommand

    {
        private ModifiedUserUseCaseValidator _validator;
        public EfModiefiedUserUseCaseCommand(AspDbContext context, ModifiedUserUseCaseValidator validator)
            : base(context)
        {
            _validator = validator;
        }
        
        public int Id => 9;

        public string Name => "Update UseCases for users";

        public string Description => "Update UseCases for users";

        public void Execute(UserUseCasesDto request)
        {
            _validator.ValidateAndThrow(request);

            var useCases = Context.UserUseCases.Where(x => x.UserId == request.UserId);

            Context.RemoveRange(useCases);

            var newUseCases = request.UseCaseIds.Select(x => new UserUseCase
            {
                UseCaseId = x,
                UserId = request.UserId
            });

            Context.AddRange(newUseCases);
            Context.SaveChanges();
        }
    }
}
