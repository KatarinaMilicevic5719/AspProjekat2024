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
    public class EfSuggestFriendQuery : IGetSuggestFriendQuery
    {
        public int Id => 12;

        public string Name => "Suggest friend";

        public string Description => "Suggest friend";
        private AspDbContext _context;
        public EfSuggestFriendQuery(AspDbContext context) => _context = context;
        public IEnumerable<SuggestFriendDto> Execute(GetSuggestFriendDto dto)
        {
            var user = _context.Users.Include(x => x.RequestedFriends)
                                     .Include(x => x.AcceptedFriends)
                                     .FirstOrDefault(x => x.Id == dto.ThisUserId);

            var requestFriendIds = user.RequestedFriends.Select(x => x.AcceptUserId);
            var acceptFriendIds = user.AcceptedFriends.Select(x => x.RequestUserId);

            var allFriends = new List<int>();
            allFriends.AddRange(requestFriendIds);
            allFriends.AddRange(acceptFriendIds);

            var friends = _context.Users.Include(x => x.RequestedFriends)
                                        .Include(x => x.AcceptedFriends)
                                        .Where(x => allFriends.Contains(x.Id)).ToList();
            
            var friendsOfFriend = new List<int>();

            friends.ForEach(x =>
            {
                var requestFriendIds = x.RequestedFriends.Select(y => y.AcceptUserId);
                var acceptFriendIds = x.AcceptedFriends.Select(y => y.RequestUserId);

                friendsOfFriend.AddRange(requestFriendIds);
                friendsOfFriend.AddRange(acceptFriendIds);
            });

            friendsOfFriend = friendsOfFriend.Distinct().ToList();

            allFriends.Add(dto.ThisUserId);

            var idsToRemove = allFriends;

            friendsOfFriend.RemoveAll(x => idsToRemove.Contains(x));

            var suggestFriend = _context.Users.Where(x => friendsOfFriend.Contains(x.Id)).ToList();

            return suggestFriend.Select(x => new SuggestFriendDto
            {
                Name = x.FirstName + " " + x.LastName,
                Email = x.Email
            }).ToList();

        }
    }
}
