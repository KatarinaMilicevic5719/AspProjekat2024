using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO.BaseDTOs;

namespace Asp.Application.UseCases.DTO
{
    public class SendMessageDto
    {
        public int UserId { get; set; }
        public int ToId { get; set; }
        public string Context { get; set; }
    }

    public class GetMessagesDto
    {
        public string Friend { get; set; }
        public string Message { get; set; }
        public bool Seen { get; set; }
    }
    public class SearchMessage : BaseSearch{
        public int FriendId { get; set; }
    }

    public class GetFriendMessageDto
    {
        public string SendBy { get; set; }
        public string Context { get; set; }
        public bool Seen { get; set; }
    }

    public class EditMessageDto
    {
        public int UserId { get; set; }
        public string Context { get; set; }
        public int MessageId { get; set; }
        public bool Seen { get; set; }
    }
}
