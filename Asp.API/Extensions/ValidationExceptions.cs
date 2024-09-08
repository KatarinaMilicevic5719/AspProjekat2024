using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Asp.API.Extensions
{
    public static class ValidationExceptions
    {
        public static UnprocessableEntityObjectResult ToUnprocessableEntity
            (this IEnumerable<ValidationFailure> errors)
        {
            var error = errors.Select(x => new
                {
                    x.ErrorMessage,
                    x.PropertyName
                });
           return new UnprocessableEntityObjectResult(error);
        }
    }
}
