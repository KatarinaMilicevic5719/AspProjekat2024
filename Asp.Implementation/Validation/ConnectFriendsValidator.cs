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
    public class ConnectFriendsValidator : AbstractValidator<FriendsDto>
    {
        private AspDbContext _context;

        public ConnectFriendsValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.RequestUserId).Cascade(CascadeMode.Stop)
                                         .NotNull().WithMessage("RequestUserId is required.")
                                         .GreaterThan(0).WithMessage("RequestUserId must be greater than 0.")
                                         .Must(id => HelperValidation.UserExists(id, _context)).WithMessage("User with Id: {PropertyValue} doesn't exist.}");

            RuleFor(x => x.AcceptUserId).Cascade(CascadeMode.Stop)
                                        .NotNull().WithMessage("AcceptUserId is required.")
                                        .GreaterThan(0).WithMessage("AcceptUserId must be greater than 0.")
                                        .Must(id => HelperValidation.UserExists(id, _context)).WithMessage("User with Id: {PropertyValue} doesn't exist.");

            RuleFor(x => x).Cascade(CascadeMode.Stop)
                            .Must(dto =>HelperValidation.AlreadyFriends(dto.AcceptUserId, dto.RequestUserId, _context))
                            .WithMessage("You are already following this person.");

            RuleFor(x => new { x.RequestUserId, x.AcceptUserId })
                        .Must(ids => ids.RequestUserId != ids.AcceptUserId)
                        .WithMessage("RequestUserId and AcceptUserId cannot be the same.");

        }

        
    }
}
