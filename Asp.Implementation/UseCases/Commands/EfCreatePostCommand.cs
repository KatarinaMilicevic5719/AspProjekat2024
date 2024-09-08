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
    public class EfCreatePostCommand : EfUseCase, ICreatePostCommand
    {
        private CreatePostValidator _validator;

        public EfCreatePostCommand(AspDbContext context, CreatePostValidator validator)
            :base(context)
        {
            _validator = validator;
        }

        public int Id => 4;

        public string Name => "Add post";

        public string Description => "Create a new post.";

        public void Execute(CreatePostsDto request)
        {
            _validator.ValidateAndThrow(request);

            {
                try
                {
                    if(!string.IsNullOrEmpty(request.ImageName))
                    {
                        var image = new Image
                        {
                            Path = request.ImageName,
                            Alt = request.Title
                        };

                        Context.Images.Add(image);
                        Context.SaveChanges();

                        var post = new Post
                        {
                            Title = request.Title,
                            Description = request.Description,
                            UserId = request.UserId,
                            ImageId = image.Id
                        };
                        
                        Context.Posts.Add(post);
                        Context.SaveChanges();

                        foreach (var category in request.CategoriesIds)
                        {
                            var categoryPost = new PostCategory
                            {
                                PostId = post.Id,
                                CategoryId = category
                            };

                            Context.PostCategories.Add(categoryPost);
                            Context.SaveChanges();

                        }
                    }
                    else
                    {
                        var post = new Post
                        {
                            Title = request.Title,
                            Description = request.Description,
                            UserId = request.UserId
                        };

                        Context.Posts.Add(post);
                        Context.SaveChanges();

                        foreach (var category in request.CategoriesIds)
                        {
                            var categoryPost = new PostCategory
                            {
                                PostId = post.Id,
                                CategoryId = category
                            };

                            Context.PostCategories.Add(categoryPost);
                            Context.SaveChanges();

                        }

                    }
                   

                }
                catch (Exception)
                {
             
                    throw;
                }
            }
        }
    }
}
