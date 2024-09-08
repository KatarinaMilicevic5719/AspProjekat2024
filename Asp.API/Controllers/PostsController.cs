using System;
using System.Collections.Generic;
using System.IO;
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
    public class PostsController : ControllerBase
    {
        private UseCaseHandler _handler;
        public PostsController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Korisnik moze da vidi sve postove svojih prijatelja i omogucena mu je paginacija i pretraga
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /api/Posts/
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // GET: api/<PostsController>
        [HttpGet]
        public IActionResult Get([FromQuery] PageSearch search,
                                 [FromServices] IApplicationUser user,
                                 [FromServices] IGetPostsQuery query)
        {
            search.UserId = user.Id;
            return Ok(_handler.HandleQuery(query, search));
        }

        /// <summary>
        /// Prosledjivanjem PostId korisnik moze da vidi sve detalje posta prijatelja
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="query"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     Get /api/Posts/1
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // GET api/<PostsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery] SearchPostsDto dto, 
                                [FromServices] IApplicationUser user, 
                                [FromServices] IGetOnePostQuery query)
        {
            dto.UserId = user.Id;
            dto.PostId = id;

            return Ok(_handler.HandleQuery(query, dto));
            
        }

        /// <summary>
        /// Korisnik moze da doda post
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Posts/ 
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        // POST api/<PostsController>
        [HttpPost]
        [Authorize]
        public IActionResult Post([FromForm] CreatePostApiDto dto,
                         [FromServices] IApplicationUser user,
                         [FromServices] ICreatePostCommand command)
        {
            var guid = Guid.NewGuid();

            var extension = Path.GetExtension(dto.Image.FileName);

            extension.ValidateExtension();

            var newFileName = guid + extension;
            var path = Path.Combine("wwwroot","Images", newFileName);

            using(var fileStream = new FileStream(path, FileMode.Create))
            {
                dto.Image.CopyTo(fileStream);
            }

            dto.UserId = user.Id;
            dto.ImageName = newFileName;
            _handler.HandleCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
            

        }

        /// <summary>
        /// Korisnik moze da izmeni svoj post
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Posts/1
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // PUT api/<PostsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id,
                        [FromForm] EditPostApiDto dto,
                        [FromServices] IApplicationUser user,
                        [FromServices] IModifiePostCommand command)
        {
            dto.UserId = user.Id;
            dto.PostId = id;

            var guid = Guid.NewGuid();

            if (!string.IsNullOrEmpty(dto.ImageName))
            {
                var extension = Path.GetExtension(dto.Image.FileName);

                extension.ValidateExtension();

                var newFileName = guid + extension;
                var path = Path.Combine("wwwroot", "Images", newFileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    dto.Image.CopyTo(fileStream);
                }

                dto.ImageName = newFileName;
            }

            dto.UserId = user.Id;
            _handler.HandleCommand(command, dto);
            return Ok();


        }

        /// <summary>
        /// Korisnik moze da obrise svoj post
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
        // DELETE api/<PostsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, 
                                    [FromServices] IApplicationUser user, 
                                    [FromServices] IDeletePostCommand command)
        {
            var dto = new DeleteDto
            {
                Email = user.Email,
                Id = id
            };

            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
