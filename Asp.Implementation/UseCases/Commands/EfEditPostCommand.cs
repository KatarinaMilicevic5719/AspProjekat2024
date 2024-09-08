using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using Asp.Domain;
using Asp.Implementation.Validation;
using FluentValidation;

namespace Asp.Implementation.UseCases.Commands
{
    public class EfEditPostCommand : EfUseCase, IModifiePostCommand
    {
        private EditPostValidator _validator;
        public EfEditPostCommand(AspDbContext context, EditPostValidator validator)
            : base(context)
        {
            _validator = validator;
        }

        public int Id => 23;

        public string Name => "Edit your post";

        public string Description => "Edit your post";

        public void Execute(EditPostDto request)
        {
            _validator.ValidateAndThrow(request);

            try
            {
                var user = Context.Users.Find(request.UserId);
                Context.User = user.Email;

                var post = Context.Posts.Find(request.PostId);

                if (post == null)
                {
                    throw new EntityNotFoundException();
                }

                if(post.UserId != request.UserId)
                {
                    throw new UseCaseConflictException("You can edit only your post");
                }

                
                post.Title = request.Title;
                post.Description = request.Description;

              
                if (!string.IsNullOrEmpty(request.ImageName))
                {
                   
                    var image = new Image
                    {
                        Path = request.ImageName,
                        Alt = request.Title
                    };
                    Context.Images.Add(image);
                    post.ImageId = image.Id;  
                }

               
                var existingCategories = Context.PostCategories.Where(pc => pc.PostId == post.Id).ToList();
                Context.PostCategories.RemoveRange(existingCategories);

                foreach (var categoryId in request.CategoriesIds)
                {
                    var categoryPost = new PostCategory
                    {
                        PostId = post.Id,
                        CategoryId = categoryId
                    };
                    Context.PostCategories.Add(categoryPost);
                }

                Context.SaveChanges(); 
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}