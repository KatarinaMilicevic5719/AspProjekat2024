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
    public class SendMessageValidator : AbstractValidator<SendMessageDto>
    {
        private readonly AspDbContext _context;
        public SendMessageValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.ToId).Cascade(CascadeMode.Stop)
                                .NotNull().WithMessage("RequestUserId is required.")
                                .GreaterThan(0).WithMessage("RequestUserId must be greater than 0.")
                                .Must(id => HelperValidation.UserExists(id, _context)).WithMessage("User with Id: {PropertyValue} doesn't exist.");
            RuleFor(x => x).Cascade(CascadeMode.Stop)
                           .Must(dto => HelperValidation.AlreadyFriends(dto.UserId, dto.ToId, _context, negate: true))
                           .WithMessage("You aren't following this person so you can't send message.");
            
            RuleFor(x => new { x.UserId, x.ToId })
                        .Must(ids => ids.UserId != ids.ToId)
                        .WithMessage("RequestUserId and AcceptUserId cannot be the same.");

            RuleFor(x => x.Context).Cascade(CascadeMode.Stop)
                                   .NotEmpty().WithMessage("Context is required.")
                                   .MaximumLength(200).WithMessage("Context must be shorter than 200 characters.");
        }
    }

  
}
