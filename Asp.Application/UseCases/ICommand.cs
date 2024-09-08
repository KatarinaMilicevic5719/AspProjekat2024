using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Application.UseCases
{
    public interface ICommand<T> : IUseCase
    {
        void Execute(T request);
    }
}
