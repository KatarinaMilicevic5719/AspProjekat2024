using Asp.Application.Logging;
using Asp.DataAccess;
using Asp.DataAccess.Migrations;
using Asp.Domain;
using Asp.Implementation.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Implementation.Logging
{
    public class DbExceptionLogger : EfUseCase,IExceptionLogger
    {
        public DbExceptionLogger(AspDbContext context) : base(context)
        {
        }

        public void Log(Exception ex)
        {
            Guid id = Guid.NewGuid();
            Domain.ErrorLog log = new Domain.ErrorLog
            {
                ErrorId = id,
                Message = ex.Message,
                StrackTrace = ex.StackTrace,
                Time = DateTime.UtcNow
            };
            Context.ErrorLogs.Add(log);
            Context.SaveChanges();
           
        }
    }
}
