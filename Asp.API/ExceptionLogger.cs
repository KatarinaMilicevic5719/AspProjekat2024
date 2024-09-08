using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.API
{
    public interface IExceptionLogger1
    {
        void LogException(Exception ex, Guid correlationId);
    }
    public class ConsoleExceptionLogger1 : IExceptionLogger1
    {
        public void LogException(Exception e, Guid correlationId)
        {
            var message = "Time: " + DateTime.UtcNow.ToLongDateString() + "\n" +
                        "Message: " + e.Message + "\n" +
                        "Stack trace: " + e.StackTrace + "\n" +
                        "Inner ex message: " + e.InnerException?.Message + "\n" +
                        "Error ID: " + correlationId.ToString();
            Console.WriteLine(message);
        }

    }
}
