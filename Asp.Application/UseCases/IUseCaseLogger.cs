using Asp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Application.UseCases
{
    public interface IUseCaseLogger
    {
        void Log(UseCaseLog log);
    }

    //public class UseCaseLog
    //{
    //    public string UseCaseName { get; set; }
    //    public string User { get; set; }
    //    public int UserId { get; set; }
    //    public DateTime ExecutionDateTime { get; set; }
    //    public string Data { get; set; }
    //    public bool IsAuthorized { get; set; }
    //}
}
