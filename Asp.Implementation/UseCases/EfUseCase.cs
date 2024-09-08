using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.DataAccess;

namespace Asp.Implementation.UseCases
{
    public abstract class EfUseCase
    {
        protected EfUseCase(AspDbContext context)
        {
            Context = context;
        }

        protected AspDbContext Context { get; }

    }
}
