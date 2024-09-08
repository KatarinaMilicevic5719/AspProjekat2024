using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using FluentValidation;

namespace Asp.Implementation.Validation
{
    public class EditUserValidator : AbstractValidator<EditUserDto>
    {
        private AspDbContext _context;

        public EditUserValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
                                     .NotEmpty().WithMessage("First name is required.")
                                     .MaximumLength(30).WithMessage("First name must not exceed 30 characters.")
                                     .Matches(@"^[A-ZŠĐŽČĆ][a-zšđžčć]{2,10}(\s[A-ZŠĐŽČĆ][a-zšđžčć]{2,10})?$")
                                        .WithMessage("First name is not in correct format.");

            RuleFor(x => x.LastName).Cascade(CascadeMode.Stop)
                                     .NotEmpty().WithMessage("Last name is required.")
                                     .MaximumLength(30).WithMessage("Last name must not exceed 30 characters.")
                                     .Matches(@"^[A-ZŠĐŽČĆ][a-zšđžčć]{2,15}(\s[A-ZŠĐŽČĆ][a-zšđžčć]{2,15})?$")
                                        .WithMessage("Last name is not in correct format.");

            RuleFor(x => x.Password).Cascade(CascadeMode.Stop)
                                    .NotEmpty().WithMessage("Password is require.")
                                    .Matches(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")
                                        .WithMessage("Password must have minimum eight characters long," +
                                        " at least one uppercase letter, one lowercase letter and one number");
        }
    }
}
