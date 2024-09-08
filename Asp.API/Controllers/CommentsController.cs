using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
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
    public class CommentsController : ControllerBase
    {
        private UseCaseHandler _handler;
        public CommentsController(UseCaseHandler handler) => _handler = handler;

        /// <summary>
        /// User moze da ostavi komentar na postovima svojih prijatelja
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Comments
        ///     {
        ///        "PostId": 1,
        ///        "Comment" : "New comment",
        ///        "ParentId" : 2
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        
        // POST api/<CommentsController>
        [HttpPost]
        public IActionResult Post([FromBody] CommentDto dto,
                                  [FromServices] IApplicationUser user,
                                  [FromServices] ICreateCommentCommand command)
        {
            dto.UserId = user.Id;

            _handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Na osnovu prosledjenog Id komentara se utvrdjuje da li pripada ulogovanom korisniku
        /// Ako pripada dozvoljeno mu je menjanje
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Comments/1
        ///     {
        ///        "PostId": 1,
        ///        "Comment" : "New comment",
        ///        "ParentId" : 2
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        // PUT api/<CommentsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] EditCommentDto dto,
                        [FromServices] IApplicationUser user,
                        [FromServices] IEditCommentCommand command)
        {
            dto.UserId = user.Id;
            dto.CommentId = id;

            _handler.HandleCommand(command, dto);
            return Ok();
        }

        /// <summary>
        /// Na osnovu prosledjenog Id komentara se utvrdjuje da li pripada ulogovanom korisniku
        /// Ako pripada dozvoljeno mu je brisanje
        /// </summary>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="204">No content.</response>
        // DELETE api/<CommentsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IApplicationUser user, [FromServices] IDeleteCommand command)
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
