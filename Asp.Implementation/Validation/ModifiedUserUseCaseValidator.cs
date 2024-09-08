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
    public class ModifiedUserUseCaseValidator : AbstractValidator<UserUseCasesDto>
    {
        private readonly AspDbContext _context;
        public ModifiedUserUseCaseValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop)
                                  .Must(x => context.Users.Any(u => u.Id == x && u.IsActive == true))
                                    .WithMessage("User with provided Id doesn't exist.");

            RuleFor(x => x.UseCaseIds).Cascade(CascadeMode.Stop)
                                      .NotEmpty().WithMessage("Use cases are required.")
                                      .Must(x => x.Distinct().Count() == x.Count())
                                        .WithMessage("Duplicates are not allowed.");
        }
    }
}
