using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.Logging;
using Asp.Application.UseCases;
using Asp.Domain;
using Newtonsoft.Json;

namespace Asp.Implementation
{
    public class UseCaseHandler
    {
        private IExceptionLogger _logger;
        private IApplicationUser _user;
        private IUseCaseLogger _useCaseLogger;

        public UseCaseHandler(IExceptionLogger logger, 
                              IApplicationUser user, 
                              IUseCaseLogger useCaseLogger)
        {
            _logger = logger;
            _user = user;
            _useCaseLogger = useCaseLogger;
        }
        public void HandleCommand<TRequest>(ICommand<TRequest> command, TRequest data)
        {
            try
            {
                HendleLoggingAndAuthorization(command, data);

                var stopWatch = new Stopwatch();
                stopWatch.Start();

                command.Execute(data);

                stopWatch.Stop();

                Console.WriteLine(command.Name + " Duration: " + stopWatch.ElapsedMilliseconds + "ms.");
            }
            catch(Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }
        
        public TResponse HandleQuery<TRequest, TResponse>(IQuery<TRequest, TResponse> query, TRequest data)
        {
            try
            {
                HendleLoggingAndAuthorization(query, data);

                var stopWatch = new Stopwatch();
                stopWatch.Start();

                var response = query.Execute(data);

                stopWatch.Stop();

                Console.WriteLine(query.Name + " Duration: " + stopWatch.ElapsedMilliseconds + " ms.");

                return response;
            }
            catch (Exception ex)
            {
                _logger.Log(ex);
                throw;
            }
        }

        private void HendleLoggingAndAuthorization<TRequest>(IUseCase useCase, TRequest data)
        {
            var isAuthorized = _user.UseCaseIds.Contains(useCase.Id);
            var log = new UseCaseLog
            {
                User = _user.Identity,
                ExecutionDateTime = DateTime.UtcNow,
                UseCaseName = useCase.Name,
                Data = JsonConvert.SerializeObject(data),
                IsAuthorized = isAuthorized
            };
            _useCaseLogger.Log(log);

           
            //if(!isAuthorized) {
            //    throw new ForbiddenUseCaseExecutionException(useCase.Name, _user.Identity);
            //}

            

           
        }
    }
}
