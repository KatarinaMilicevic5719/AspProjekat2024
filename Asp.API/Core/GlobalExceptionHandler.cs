using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Application.Exceptions;
using Asp.Application.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Asp.API.Core
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(System.Exception ex)
            {
                
                httpContext.Response.ContentType = "application/json";
                
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;

                if(ex is ForbiddenUseCaseExecutionException)
                {
                    statusCode = StatusCodes.Status403Forbidden;
                }
                if(ex is EntityNotFoundException)
                {
                    statusCode = StatusCodes.Status404NotFound;
                }
                if(ex is ValidationException e)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    if(ex.Message.Contains("Invalid file extension. They must be .png or .jpeg or .jpg, .giff"))
                    {
                        response = new
                        {
                            Image=ex.Message
                        };
                    }
                    else
                    {
                        response = new
                        {
                            errors = e.Errors.Select(x => new
                            {
                                property = x.PropertyName,
                                error = x.ErrorMessage
                            })
                        };
                    }
                   
                }

                if(ex is UseCaseConflictException conEx)
                {
                    statusCode = StatusCodes.Status409Conflict;
                    response = new { message = conEx.Message };
                }

                if(ex is UnauthorizedAccessException UnAuthEx)
                {
                    statusCode = StatusCodes.Status403Forbidden;
                    response = new { message = UnAuthEx.Message };
                }

                httpContext.Response.StatusCode = statusCode;

                if (response != null)
                {
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }
}
