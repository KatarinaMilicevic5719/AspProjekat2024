using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;

namespace Asp.Application.UseCases.Queries
{
    public interface IGetOnePostQuery : IQuery<SearchPostsDto, IEnumerable<GetFriendPostDto>>
    {
    }
}
