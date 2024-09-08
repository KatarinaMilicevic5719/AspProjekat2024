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
    public class EfDeleteCategoryCommand : EfUseCase, IDeleteCommand
    {
        private DeleteCategoryValidator _validator;

        public EfDeleteCategoryCommand(AspDbContext context, DeleteCategoryValidator validator) 
            : base (context)
        {
           
            _validator = validator;
        }
        public int Id => 6;

        public string Name => "Delete category";

        public string Description => "Delete category sending id.";

        public void Execute(DeleteDto request)
        {
            _validator.ValidateAndThrow(request);

            Context.User = request.Email;

            var category = Context.Categories.Find(request.Id);

            if (category == null || category.IsActive == false)
            {
                throw new EntityNotFoundException();
            }

            var postCat = Context.PostCategories.Where(x => x.CategoryId == request.Id).ToList();

            Context.RemoveRange(postCat);
            Context.Remove(category);
            Context.SaveChanges();
        }

        
    }
}
