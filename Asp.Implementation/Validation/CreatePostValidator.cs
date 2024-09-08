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
    public class CreatePostValidator : AbstractValidator<CreatePostsDto>
    {
        private AspDbContext _context;

        public CreatePostValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.Title).Cascade(CascadeMode.Stop)
                                 .NotEmpty().WithMessage("Title is required.")
                                 .MinimumLength(5).WithMessage("Title must be longer then 5 characters.")
                                 .MaximumLength(50).WithMessage("Title must be shorter then 50 characters.")
                                 .Must((dto, title) => HelperValidation.TitleNotInUse(title, dto.UserId, _context))
                                    .WithMessage("Post with title {PropertyValue} already exists for this user.");

            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
                                       .NotEmpty().WithMessage("Context is required.")
                                       .MinimumLength(10).WithMessage("Context must be at least 10 characters long.")
                                       .MaximumLength(200).WithMessage("Context must be shorter then 200 characters.");

            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop)
                                  .NotNull().WithMessage("UserId is required.")
                                  .Must(id => HelperValidation.UserExists(id, _context)).WithMessage("User with id {PropertyValue} doesn't exist.");

            RuleFor(x => x.CategoriesIds).Cascade(CascadeMode.Stop)
                                         .NotEmpty().WithMessage("Category is required.")
                                         .Must(catIds => HelperValidation.CategoriesExist(catIds, _context)).WithMessage("At least one category does not exist.");
                                        
            
        }
    }

    public class EditPostValidator : AbstractValidator<EditPostDto>
    {
        private AspDbContext _context;

        public EditPostValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.Title).Cascade(CascadeMode.Stop)
                                 .NotEmpty().WithMessage("Title is required.")
                                 .MinimumLength(5).WithMessage("Title must be longer then 5 characters.")
                                 .MaximumLength(50).WithMessage("Title must be shorter then 50 characters.")
                                 .Must((dto, title) => HelperValidation.TitleNotInUse(title, dto.UserId, _context))
                                    .WithMessage("Post with title {PropertyValue} already exists for this user.");

            RuleFor(x => x.Description).Cascade(CascadeMode.Stop)
                                       .NotEmpty().WithMessage("Context is required.")
                                       .MinimumLength(10).WithMessage("Context must be at least 10 characters long.")
                                       .MaximumLength(200).WithMessage("Context must be shorter then 200 characters.");

            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop)
                                  .NotNull().WithMessage("UserId is required.")
                                  .Must(id => HelperValidation.UserExists(id, _context)).WithMessage("User with id {PropertyValue} doesn't exist.");

            RuleFor(x => x.CategoriesIds).Cascade(CascadeMode.Stop)
                                         .NotEmpty().WithMessage("Category is required.")
                                         .Must(catIds => HelperValidation.CategoriesExist(catIds, _context)).WithMessage("At least one category does not exist.");


        }
    }
}
