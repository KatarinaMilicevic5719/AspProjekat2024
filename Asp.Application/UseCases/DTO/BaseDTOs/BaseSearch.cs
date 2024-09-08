using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Application.UseCases.DTO.BaseDTOs
{
    public class BaseSearch
    {
        public string Keyword { get; set; }
        public int UserId { get; set; }
    }

    public class PageSearch : BaseSearch
    {
        public int? PerPage { get; set; } = 2;
        public int? Page { get; set; } = 1;
    }

    
}
