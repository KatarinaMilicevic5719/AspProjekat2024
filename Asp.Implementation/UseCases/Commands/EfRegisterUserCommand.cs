using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Emails;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using Asp.Domain;
using Asp.Implementation.Helper;
using Asp.Implementation.Validation;
using FluentValidation;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {

        private RegisterUserValidator _validator;
        private IEmailSender _sender;

        public EfRegisterUserCommand(AspDbContext context, 
            RegisterUserValidator validator, IEmailSender sender)
            :base(context)
        {
            _validator = validator;
            _sender = sender;
        }

        public int Id => 1;

        public string Name => "User register";

        public string Description => "User registeration using entity framework.";

        public void Execute(RegisterDto request)
        {
            _validator.ValidateAndThrow(request);

            var user = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Password = PasswordHelper.HashPassword(request.Password)
            };
            
            Context.Users.Add(user);
            Context.SaveChanges();
            
            _sender.Send(new EmailMessage
            {
                
                To = request.Email,
                Title = "Confirm registration",
                Body = "Dear .."
            });
            
        }
    }
}
