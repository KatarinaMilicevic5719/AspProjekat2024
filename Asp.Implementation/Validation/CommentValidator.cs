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
    public class CommentValidator : AbstractValidator<CommentDto>
    {
        private AspDbContext _context;
        public CommentValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.Comment).Cascade(CascadeMode.Stop)
                                   .NotEmpty().WithMessage("Comment context is required.")
                                   .MaximumLength(150).WithMessage("Maximum is 150 characters.");

            RuleFor(x => x.PostId).Cascade(CascadeMode.Stop)
                                  .NotEmpty().WithMessage("PostId is required.")
                                  .Must(id => HelperValidation.PostExists(id, _context))
                                    .WithMessage("Post with id {PropertyValue} doesn't exist.");

            RuleFor(x => x.ParentId).Cascade(CascadeMode.Stop)
                                    .Must(id => HelperValidation.CommentExist(id, _context))
                                        .WithMessage("Parent comment with id {PropertyValue} doesn't exist.");
        }
    }

    public class EditCommentValidator : AbstractValidator<EditCommentDto>
    {
        private AspDbContext _context;
        public EditCommentValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.Comment).Cascade(CascadeMode.Stop)
                                   .NotEmpty().WithMessage("Comment context is required.")
                                   .MaximumLength(150).WithMessage("Maximum is 150 characters.");

            RuleFor(x => x.PostId).Cascade(CascadeMode.Stop)
                                  .NotEmpty().WithMessage("PostId is required.")
                                  .Must(id => HelperValidation.PostExists(id, _context))
                                    .WithMessage("Post with id {PropertyValue} doesn't exist.");

            RuleFor(x => x.ParentId).Cascade(CascadeMode.Stop)
                                    .Must(id => HelperValidation.CommentExist(id, _context))
                                        .WithMessage("Parent comment with id {PropertyValue} doesn't exist.");

            RuleFor(x => x.CommentId).Cascade(CascadeMode.Stop)
                                     .Must(id => HelperValidation.CommentExist(id, _context))
                                        .WithMessage("Comment with id {PropertyValue} doesn't exist.");
        }
    }
}
