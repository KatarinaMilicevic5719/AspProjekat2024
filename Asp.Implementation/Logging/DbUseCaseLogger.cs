using Asp.Application.UseCases;
using Asp.DataAccess;
using Asp.Domain;
using Asp.Implementation.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Implementation.Logging
{
    public class DbUseCaseLogger : EfUseCase,IUseCaseLogger
    {
        public DbUseCaseLogger(AspDbContext context) : base(context)
        {
        }

        public void Log(UseCaseLog log)
        {
            UseCaseLog usecaseLog = new UseCaseLog
            {
                UseCaseName = log.UseCaseName,
                User=log.User,
                Data=log.Data,
                ExecutionDateTime=log.ExecutionDateTime,
                IsAuthorized=log.IsAuthorized,
                   
            };
            Context.UseCaseLogs.Add(usecaseLog);
            Context.SaveChanges();
        }
    }
}
