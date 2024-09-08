using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.Application.UseCases.Queries;
using Asp.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Asp.Implementation.UseCases.Queries.EF
{
    public class EfGetPostsQuery : IGetPostsQuery
    {
        public int Id => 5;

        public string Name => "Get friend's post";

        public string Description => "Get all posts from your friend.";

        private AspDbContext _context;
        public EfGetPostsQuery(AspDbContext context) => _context = context;

        public PagedResponse<GetPostsDto> Execute(PageSearch search)
        {
            var friends = _context.UserFriends
                               .AsEnumerable()
                               .Where(x => x.AcceptUserId == search.UserId || x.RequestUserId == search.UserId)
                               .SelectMany(x => new List<int> { x.AcceptUserId, x.RequestUserId })
                               .Distinct()
                               .Where(id => id != search.UserId);

            var posts = _context.Posts.Include(u => u.User)
                                      .Include(l => l.PostLikes)
                                      .Include(c => c.PostComments)
                                      .Include(pc => pc.PostCategories)
                                      .ThenInclude(c => c.Category).AsQueryable();

            posts = posts.Where(x => friends.Contains(x.UserId) && x.IsActive == true);
            

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                posts = posts.Where(x => x.Title.Contains(search.Keyword) 
                            || x.User.FirstName.Contains(search.Keyword)
                            || x.User.LastName.Contains(search.Keyword));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.PerPage = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<GetPostsDto>();
            response.TotalCount = posts.Count();
            response.Data = posts.Skip(toSkip).Take(search.PerPage.Value).Select(x => new GetPostsDto
            {
                Title = x.Title,
                Description = x.Description,
                ImagePath = _context.Images.Where(y => y.Id == x.ImageId).Select(p => p.Path).FirstOrDefault(),
                Creator = x.User.FirstName + " " + x.User.LastName,
                CreatedAt = x.CreatedAt,
                Likes = x.PostLikes.Where(x => x.IsActive == true).Count(),
                Comments = x.PostComments.Where(x => x.IsActive == true).Count()
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;
        }

       
    }
}
