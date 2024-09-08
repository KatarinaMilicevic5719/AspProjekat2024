using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases;
using Asp.Domain;

namespace Asp.Implementation.UseCases.UseCaseLoggers
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(UseCaseLog log)
        {
            Console.WriteLine($"UseCase: {log.UseCaseName}\n " +
                              $"User: {log.User}\n " +
                              $"Authorized:  { log.IsAuthorized}\n " +
                              $"ExecutionDateTime: {log.ExecutionDateTime}\n" +
                              $"Data: {log.Data}\n");
        }
    }
}
