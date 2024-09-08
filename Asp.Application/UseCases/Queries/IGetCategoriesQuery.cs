﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;

namespace Asp.Application.UseCases.Queries
{
    public interface IGetCategoriesQuery : IQuery<BaseSearch, IEnumerable<CategoryDTO>>
    {
    }
}
