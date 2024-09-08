using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.DataAccess;
using Asp.Implementation.Validation;
using FluentValidation;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfModifiedCategoryCommand : EfUseCase, IModifiedCategoryCommand
    {
        private readonly ModifiedCategoryValidator _validator;
        public EfModifiedCategoryCommand(AspDbContext context, ModifiedCategoryValidator validator)
                : base(context)
        {
            _validator = validator;
        }
        public int Id => 7;

        public string Name => "Edit category";

        public string Description => "Editing category information";

        public void Execute(ModifiedCategoryDto request)
        {
            _validator.ValidateAndThrow(request);

            Context.User = request.Email;

            var category = Context.Categories.Find(request.Id);

            if(category == null || category.IsActive == false)
            {
                throw new EntityNotFoundException();
            }

            category.CategoryName = request.CategoryName;

            Context.Categories.Update(category);
            Context.SaveChanges();
        }

    }
}
