using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.Application.UseCases.Queries;
using Asp.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace Asp.Implementation.UseCases.Queries.EF
{
    public class EfGetUserProfileQuery : IGetUserProfileQuery
    {
        private readonly AspDbContext _context;
        public EfGetUserProfileQuery(AspDbContext context) => _context = context;
        public int Id => 25;

        public string Name => "View friend's profile";

        public string Description => "All the details friend's post";

        public IEnumerable<UserProfileDto> Execute(SearchUser search)
        {

            var isFriend = _context.UserFriends.Where(x => x.RequestUserId == search.UserId && x.AcceptUserId == search.UserProfileId
                                                      || x.AcceptUserId == search.UserId && x.RequestUserId == search.UserProfileId)
                                                     .FirstOrDefault();

            if(isFriend == null)
            {
                throw new UseCaseConflictException("You can see only your friend's profile.");
            }

            var user = _context.Users.Include(x => x.AcceptedFriends)
                                     .Include(x => x.RequestedFriends)
                                     .Include(x => x.UserPosts)
                                        .ThenInclude(x => x.PostComments)
                                    .Include(x => x.UserPosts)
                                        .ThenInclude(x => x.PostLikes)
                                    .Include(x => x.UserPosts)
                                        .ThenInclude(x => x.PostCategories)
                                    .FirstOrDefault(x => x.Id == search.UserProfileId);


            var profile = new UserProfileDto
            {
                Name = user.FirstName + " " + user.LastName,
                Email = user.Email,
                Friends = user.AcceptedFriends.Count() + user.RequestedFriends.Count(),
                Posts = user.UserPosts.Select(x => new GetPostsDto
                {
                    Title = x.Title,
                    Description = x.Description,
                    ImagePath = _context.Images.Where(y => y.Id == x.ImageId).Select(p => p.Path).FirstOrDefault(),
                    Creator = x.User.FirstName + " " + x.User.LastName,
                    CreatedAt = x.CreatedAt,
                    Likes = x.PostLikes.Where(x => x.IsActive == true).Count(),
                    Comments = x.PostComments.Where(x => x.IsActive == true).Count()

                }).ToList()
            };

            return new List<UserProfileDto> { profile };
        }
    }
}
