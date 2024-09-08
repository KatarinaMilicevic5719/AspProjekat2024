using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.API.DTO;
using Asp.API.Extensions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.Application.UseCases.Queries;
using Asp.DataAccess;
using Asp.Domain;
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
    [Authorize]
    public class ConnectUsersController : ControllerBase
    {
        private UseCaseHandler _handler;

        public ConnectUsersController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Na osnovu id trenutno ulogovanog usera prikazuju se svi predlozeni prijatelji
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="query"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // GET: api/<ConnectUsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] GetSuggestFriendDto dto,
                                 [FromServices] IApplicationUser user,
                                 [FromServices] IGetSuggestFriendQuery query)
        {
            dto.ThisUserId = user.Id;

            return Ok(_handler.HandleQuery(query, dto));
        }

        /// <summary>
        /// Ulogovani user moze da salje zahtev drugim korisnicima sa kojima jos uvek nije prijatelj
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/ConnectUsers
        ///     {
        ///        "AcceptUserId" : 1
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        // POST api/<ConnectUsersController>
        [HttpPost]
        public IActionResult Post([FromBody] FriendsDto dto,
                                  [FromServices] IApplicationUser user,
                                  [FromServices] IConnectFriendsCommand command)
        {
            try
            {
                var friends = new FriendsDto
                {
                    RequestUserId = user.Id,
                    AcceptUserId = dto.AcceptUserId.GetValueOrDefault()
                };

                _handler.HandleCommand(command,friends);
                return StatusCode(StatusCodes.Status201Created);

            }
            catch (ValidationException e)
            {
                return e.Errors.ToUnprocessableEntity();
            }
            catch (System.Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Na osnovu prosledjenih Id-jeva korisnik moze da obrise svog prijatelja, odnosno zahtev
        /// </summary>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Comments/1
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="204">No content.</response>
        // DELETE api/<ConnectUsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, 
                           [FromServices] IApplicationUser user, 
                           [FromServices] IDeleteCommand command)
        {
            var dto = new DeleteDto
            {
                Id = id,
                Email = user.Email
            };

            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
