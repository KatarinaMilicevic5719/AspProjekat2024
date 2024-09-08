using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.API.Extensions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.Implementation;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {

        private UseCaseHandler _handler;

        public RegisterController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Create new user.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Register
        ///     {
        ///        "FirstName": "John",
        ///        "LastName": "Doe",
        ///        "Email" : "johndoe@domain.com",
        ///        "Password" : "JohnDoe123"
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        // POST api/<RegisterConnroller>
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] RegisterDto dto, 
                                  [FromServices] IRegisterUserCommand command)
        {
            try
            {
                _handler.HandleCommand(command, dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(ValidationException e)
            {
                return e.Errors.ToUnprocessableEntity();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        
    }
}
