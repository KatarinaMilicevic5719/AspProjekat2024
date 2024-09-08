using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;
using Microsoft.AspNetCore.Http;

namespace Asp.API.DTO
{
    public class CreatePostApiDto : CreatePostsDto
    {
        public IFormFile Image { get; set; }
    }

    public class EditPostApiDto : EditPostDto
    {
        public IFormFile Image { get; set; }
    }
}
