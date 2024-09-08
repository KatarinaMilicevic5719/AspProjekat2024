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
    public class EfGetMessagesQuery : IGetMessagesQuery
    {
        private AspDbContext _context;
        public EfGetMessagesQuery(AspDbContext context) => _context = context;
        public int Id => 17;

        public string Name => "Get last messages from your friend";

        public string Description => "Get last messages from your friend";

        public IEnumerable<GetMessagesDto> Execute(BaseSearch search)
        {
            var friends = _context.UserFriends
                        .Where(x => x.RequestUserId == search.UserId || x.AcceptUserId == search.UserId)
                         .Select(x => new
                         {
                             FriendId = x.RequestUserId == search.UserId ? x.AcceptUserId : x.RequestUserId,
                             x.RequestUserId,
                             x.AcceptUserId
                         }).ToList();

            var groupedMessages = _context.Messages
                                    .AsEnumerable()
                                    .GroupBy(m => new { m.FromUserId, m.ToUserId })
                                    .ToList();

            var orderedMessages = _context.Messages
                .AsEnumerable()
                                    .GroupBy(m => new { m.FromUserId, m.ToUserId })
                                    .OrderByDescending(g => g.Max(m => m.DeletedAt))
                                    .ToList();

            var latestMessages = _context.Messages
                .AsEnumerable()
                                    .GroupBy(m => new { m.FromUserId, m.ToUserId })
                                    .Select(g => g.OrderByDescending(m => m.CreatedAt).FirstOrDefault())
                                    .ToList();

            // Pronađi podatke prijatelja
            var friendIds = friends.Select(f => f.FriendId).ToList();
            var friendDetails = _context.Users
                .Where(u => friendIds.Contains(u.Id))
                .ToDictionary(u => u.Id, u => new { u.FirstName, u.LastName });

            return latestMessages.Select(m =>
            {
                // Odredi koji je prijatelj poslao ili primio poruku
                var friendId = m.FromUserId == search.UserId ? m.ToUserId : m.FromUserId;

                // Uzmite ime i prezime prijatelja
                var friend = friendDetails.GetValueOrDefault(friendId);

                return new GetMessagesDto
                {
                    Friend = friend != null ? $"{friend.FirstName} {friend.LastName}" : "Unknown",
                    Message = m.Context,
                    Seen = m.Seen
                };
            }).ToList();

           

        }
    }
}
