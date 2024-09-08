using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using Asp.Domain;
using Asp.Implementation.Validation;
using FluentValidation;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfCreateCategoryCommand : EfUseCase, ICreateCategoryCommand
    {
        private CategoryValidator _validator;
        public EfCreateCategoryCommand(AspDbContext context, CategoryValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 2;

        public string Name => "Create category EF.";

        public string Description => "Create category using entity framework.";

        public void Execute(CreateCategoryDto request)
        {
            _validator.ValidateAndThrow(request);
            
            var category = new Category
            {
                CategoryName = request.CategoryName
            };

            Context.Categories.Add(category);
            Context.SaveChanges();
        }
    }
}