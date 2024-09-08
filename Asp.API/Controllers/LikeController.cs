using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
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
    public class LikeController : ControllerBase
    {
        private UseCaseHandler _handler;
        public LikeController(UseCaseHandler handler)
        {
            _handler = handler;
        }


        /// <summary>
        /// Ulogovani korisnik moze da lajkuje objave svojih prijatelja
        /// </summary>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Like/1
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        // POST api/<LikeController>
        [HttpPost]
        public IActionResult Post([FromBody] int postId,
                         [FromServices] IApplicationUser user,
                         [FromServices] IAddLikeCommand command)
        {
            var dto = new LikeDto
            {
                PostId = postId,
                UserId = user.Id
            };

            _handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Ulogovani korisnik moze da dislike-uje objavu koje je prethodno like-ovao
        /// </summary>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Like/1
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="204">No content.</response>
        // DELETE api/<LikeController>/5
        [HttpDelete]
        public IActionResult Delete([FromBody]int postId,
                           [FromServices] IApplicationUser user,
                           [FromServices] IDislikeCommand command)
        {
            var dto = new LikeDto
            {
                PostId = postId,
                UserId = user.Id
            };

            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
