using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO.BaseDTOs;

namespace Asp.Application.UseCases.DTO
{
    public class CreatePostsDto
    {
          public int UserId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string ImageName { get; set; }
            public IEnumerable<int> CategoriesIds { get; set; }
        
    }

    public class GetPostsDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
    }
    public class SearchPostsDto : BaseSearch
    {
        public int PostId { get; set; }
    }
    public class GetFriendPostDto
    {
        public string Title { get; set; }
        public string Context { get; set; }
        public string ImagePath { get; set; }
        public string Creator { get; set; }
        public ICollection<CreateCategoryDto> Catgoeries { get; set; }
        public ICollection<GetComment> Comments { get; set; }
        public ICollection<LikePost> Likes { get; set; }
    }

    public class EditPostDto : CreatePostsDto
    {
        public int PostId { get; set; }
    }
}
