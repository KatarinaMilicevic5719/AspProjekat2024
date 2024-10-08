﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Application.UseCases
{
    public interface IUseCase
    {
        public int Id { get; }
        string Name { get; }
        string Description { get; }
    }
}
