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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private UseCaseHandler _handler;
        public MessageController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Ulogovani korisnik moze da vidi poslednje razmenjenu poruku sa svakim od svojih prijatelja
        /// </summary>
        /// <param name="search"></param>
        /// <param name="user"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // GET: api/<MessageController>
        [HttpGet]
        public IActionResult Get([FromQuery] BaseSearch search,
                                 [FromServices] IApplicationUser user,
                                 [FromServices] IGetMessagesQuery query)
        {
            search.UserId = user.Id;
            return Ok(_handler.HandleQuery(query, search));
        }

        /// <summary>
        /// Korisnik moze na osnovu prosledjenog FriendId-ja da vidi sve poruke koje ima sa tim prijateljem
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="query"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Message/1
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromBody] SearchMessage dto, 
                                 [FromServices] IApplicationUser user,
                                 [FromServices] IGetFriendMessagesQuery query)
        {
            dto.FriendId = id;
            dto.UserId = user.Id;
            return Ok(_handler.HandleQuery(query, dto));
        }

        /// <summary>
        /// Ulogovani korisnik moze da salje poruke svojim prijateljima
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Message/
        ///     {
        ///        "Context" : "Message context"
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        // POST api/<MessageController>
        [HttpPost]
        public IActionResult Post([FromBody] SendMessageDto dto,
                                  [FromServices] IApplicationUser user,
                                  [FromServices] ISendMessageCommand command)
        {
            dto.UserId = user.Id;
            _handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Korisnik moze da menja poruke koje je poslao
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Message/1
        ///     {
        ///        "Context": "Message context",
        ///        "Seen" : true
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // PUT api/<MessageController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditMessageDto dto, 
                                 [FromServices] IApplicationUser user,
                                 [FromServices] IEditMessageCommand command)
        {
            dto.MessageId = id;
            dto.UserId = user.Id;

            _handler.HandleCommand(command, dto);
            return Ok();
        }

        /// <summary>
        /// Korisnik moze da obrise poruke
        /// </summary>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Message/1
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="204">No content.</response>
        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, 
                           [FromServices] IApplicationUser user,
                           [FromServices] IDeleteMessageCommand command)
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
