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
    public class EfGetFriendMessagesQuery : IGetFriendMessagesQuery
    {
        private readonly AspDbContext _context;
        public EfGetFriendMessagesQuery(AspDbContext context) => _context = context;
        public int Id => 18;

        public string Name => "Get message from the friend.";

        public string Description => "Get messages from the friend.";

        public IEnumerable<GetFriendMessageDto> Execute(SearchMessage search)
        {
            // Prvo proverite da li su korisnici prijatelji
            bool areFriends = _context.UserFriends
                .Any(uf => (uf.RequestUserId == search.FriendId && uf.AcceptUserId == search.UserId) ||
                            (uf.RequestUserId == search.UserId && uf.AcceptUserId == search.FriendId) &&
                            uf.IsActive);

            if (!areFriends)
            {
                throw new UseCaseConflictException("The users are not friends.");
            }

            // Ako su prijatelji, dohvatite sve poruke između njih
            var messages = _context.Messages
                .Where(m => (m.FromUserId == search.FriendId && m.ToUserId == search.UserId) ||
                            (m.FromUserId == search.UserId && m.ToUserId == search.FriendId))
                .OrderBy(m => m.CreatedAt) // Sortirajte poruke po datumu slanja
                .ToList();

            return messages.Select(x => new GetFriendMessageDto
            {
                SendBy = _context.Users.Where(y => y.Id == x.FromUserId)
                                       .Select(n => n.FirstName + " " + n.LastName)
                                       .FirstOrDefault(),
                Context = x.Context,
                Seen = x.Seen
            }).ToList();
        }
    }
}
