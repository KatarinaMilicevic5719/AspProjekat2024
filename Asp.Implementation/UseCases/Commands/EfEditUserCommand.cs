using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using Asp.Implementation.Helper;
using Asp.Implementation.Validation;
using FluentValidation;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfEditUserCommand : EfUseCase, IEditProfileCommand
    {
        private EditUserValidator _validator;
        public EfEditUserCommand(AspDbContext context, EditUserValidator validator) 
            : base(context)
        {
            _validator = validator;
        }
        public int Id => 26;

        public string Name => "Edit your profile";

        public string Description => "Edit your personal information";

        public void Execute(EditUserDto request)
        {
            _validator.ValidateAndThrow(request);

            if(request.Id != request.UserId)
            {
                throw new UseCaseConflictException("You can edit only your own profile.");
            }

            var user = Context.Users.Find(request.Id);

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Password = PasswordHelper.HashPassword(request.Password);

            Context.User = user.Email;

            Context.Users.Update(user);
            Context.SaveChanges();
        }
    }
}
