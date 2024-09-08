using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO.BaseDTOs;

namespace Asp.Application.UseCases.DTO
{
    public class RegisterDto
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

    public class FriendsDto
        {
            public int? RequestUserId { get; set; }
            public int? AcceptUserId { get; set; }
        }
    public class SuggestFriendDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
    public class GetSuggestFriendDto : BaseSearch
    {
        public int ThisUserId { get; set; }
    }
    public class SearchUser : BaseSearch
    {
        public int UserProfileId { get; set; }
    }
    public class UserProfileDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Friends { get; set;}
        public ICollection<GetPostsDto> Posts { get; set;}
    }

    public class EditUserDto 
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
