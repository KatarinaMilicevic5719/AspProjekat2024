using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.Queries;
using Asp.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Asp.Implementation.UseCases.Queries.EF
{
    public class EfGetOnePostQuery : IGetOnePostQuery
    {
        private readonly AspDbContext _context;
        public EfGetOnePostQuery(AspDbContext context) => _context = context;
        public int Id => 21;

        public string Name => "Get details about one post";

        public string Description => "Get details about one post";

        public IEnumerable<GetFriendPostDto> Execute(SearchPostsDto search)
        {
            var post = _context.Posts.Include(x => x.User)
                                     .Include(x => x.Image)
                                     .Include(x => x.PostCategories)
                                     .Include(x => x.PostComments)
                                        .ThenInclude(c => c.ChildComments)
                                     .Include(x => x.PostLikes)
                                     .FirstOrDefault(x => x.Id == search.PostId && x.IsActive == true);
                                    

            if(post == null)
            {
                throw new EntityNotFoundException();
            }

            var isFriend = _context.UserFriends.Any(uf =>
                                (uf.RequestUserId == search.UserId && uf.AcceptUserId == post.UserId) ||
                                (uf.AcceptUserId == search.UserId && uf.RequestUserId == post.UserId)
    );

            if (!isFriend)
            {
                throw new UseCaseConflictException("The post creator is not a friend.");
            }

            // Ako je kreator prijatelj, vratite sve podatke o postu
            var result = new GetFriendPostDto
            {
                Title = post.Title,
                Context = post.Description,
                ImagePath = post.Image?.Path,
                Creator = post.User.FirstName + " " + post.User.LastName,
                Catgoeries = post.PostCategories.Select(pc => new CreateCategoryDto
                {
                    CategoryName = pc.Category.CategoryName
                }).ToList(),
                Comments = post.PostComments.Where(act => act.IsActive == true)
                .Select(pc => new GetComment
                {
                    Context = pc.Context,
                    User = pc.User.FirstName + " " + pc.User.LastName,
                    Children = pc.ChildComments.Where(act => act.IsActive == true)
                    .Select(child => new ChildComment
                    {
                        Context = child.Context,
                        User = child.User.FirstName + " " + child.User.LastName,
                        Children = new List<ChildComment>()
                    }).ToList()
                }).ToList(),
                Likes = post.PostLikes.Where(act => act.IsActive == true)
                .Select(like => new LikePost
                {
                    UserLike = like.User.FirstName + " " + like.User.LastName
                }).ToList()
            };

            return new List<GetFriendPostDto> { result };
        }
    }
}
