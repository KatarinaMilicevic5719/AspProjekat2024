using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;
using static Asp.Application.UseCases.DTO.CreatePostsDto;

namespace Asp.Application.UseCases.Commands
{
    public interface ICreatePostCommand : ICommand<CreatePostsDto>
    {

    }
}
