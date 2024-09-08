using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.Application.UseCases.Queries;
using Asp.Domain;
using Asp.Implementation;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UseCaseHandler _handler;
        public UserController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Korisnik moze da udje na profil prijatelja i da vidi informacije o njemu kao i postove
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="query"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/User/1 
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, 
                         [FromQuery] SearchUser dto,
                         [FromServices] IApplicationUser user,
                         [FromServices] IGetUserProfileQuery query)
        {
            dto.UserProfileId = id;
            dto.UserId = user.Id;

            return Ok(_handler.HandleQuery(query, dto)); 
        }

        /// <summary>
        /// Korisnik moze da edituje svoj nalog
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/User/ 1
        ///     {
        ///         "FirstName" : "John"
        ///         "LastName" : "Doe",
        ///         "Password" : "John1234"
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,
                        [FromBody] EditUserDto dto,
                        [FromServices] IApplicationUser user,
                        [FromServices] IEditProfileCommand command)
        {
            dto.Id = id;
            dto.UserId = user.Id;

            _handler.HandleCommand(command, dto);
            return Ok();
        }

        /// <summary>
        /// Korisnik moze da obrise svoj nalog
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/User/1
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="204">No content.</response>
        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id,
                           [FromBody] DeleteDto dto,
                           [FromServices] IApplicationUser user,
                           [FromServices] IDeleteUserCommand command)
        {
            dto.Id = id;
            dto.Email = user.Email;

            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
