using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.DataAccess;
using FluentValidation;

namespace Asp.Implementation.Validation
{
    public class CategoryValidator : AbstractValidator<CreateCategoryDto>
    {
        private AspDbContext _context;
        public CategoryValidator(AspDbContext context)
        {
            _context = context;
            RuleFor(x => x.CategoryName).Cascade(CascadeMode.Stop)
                                        .NotEmpty().WithMessage("Category name is required.")
                                        .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
                                        .Must(name => HelperValidation.CategoryNotInUse(name, _context))
                                            .WithMessage("Category {PropertyValue} already exists.");
        }

        
    }

    public class DeleteCategoryValidator : AbstractValidator<DeleteDto>
    {
        private AspDbContext _context;

        public DeleteCategoryValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
                             .NotNull().WithMessage("Please sent post id.")
                             .GreaterThan(0).WithMessage("Id must be greater than 0.")
                             .Must(id => HelperValidation.CategoryExists(id, _context))
                                .WithMessage("Category with id {PropertyValue} doesn't exist.");
        }

    }

    public class ModifiedCategoryValidator : AbstractValidator<ModifiedCategoryDto>
    {
        private readonly AspDbContext _context;

        public ModifiedCategoryValidator(AspDbContext context)
        {
            _context = context;

            RuleFor(x => x.Id).Cascade(CascadeMode.Stop)
                              .NotEmpty().WithMessage("Id is required.")
                              .GreaterThan(0).WithMessage("Id must be greater than 0.")
                              .Must(id => HelperValidation.CategoryExists(id, _context))
                                .WithMessage("Category with id {PropertyValue} doesn't exist.");

            RuleFor(x => x.CategoryName).Cascade(CascadeMode.Stop)
                                        .NotEmpty().WithMessage("Category name is required.")
                                        .MinimumLength(3).WithMessage("Category name must be at least 3 characters long.")
                                        .Must(name => HelperValidation.CategoryNotInUse(name, _context))
                                            .WithMessage("Category {PropertyValue} already exists.");
        }

        
    }

    
}
