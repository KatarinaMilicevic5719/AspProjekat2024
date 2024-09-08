using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.API.Extensions;
using Asp.Application.UseCases.Commands;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.Application.UseCases.Queries;
using Asp.Domain;
using Asp.Implementation;
using Asp.Implementation.UseCases.Commands;
using Asp.Implementation.Validation;
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
    public class CategoriesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public CategoriesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Dohvati sve kategorije.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // GET: api/<CategoriesController>
        [HttpGet]
        public IActionResult Get([FromQuery] BaseSearch search,
                                 [FromServices] IGetCategoriesQuery query,
                                 [FromServices] IApplicationUser user)
        {
            search.UserId = user.Id;
            return Ok(_handler.HandleQuery(query, search));
        }


        /// <summary>
        /// Dodaje novu kategoriju.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Categories
        ///     {
        ///        "CategoryName": "New Category",
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        // POST api/<CategoriesController>
        [HttpPost]
        public IActionResult Post([FromBody] CreateCategoryDto dto, 
            [FromServices] ICreateCategoryCommand command)
        {
            
                _handler.HandleCommand(command, dto);
                return StatusCode(StatusCodes.Status201Created);
            
        }

        /// <summary>
        /// Edit naziva kategorije.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/Categories/1
        ///     {
        ///        "CategoryName": "New Category",
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // POST api/<CategoriesController>
        // PUT api/<CategoriesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, 
                                [FromBody] ModifiedCategoryDto dto,
                                [FromServices] IApplicationUser user,
                                [FromServices] IModifiedCategoryCommand command)
        {
            dto.Id = id;
            dto.Email = user.Email;

            _handler.HandleCommand(command, dto);
            return Ok();
        }

        /// <summary>
        /// Soft delete kategorije, prebacuje je na isActive = false i belezi vreme i izvrsioca komande
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /api/Categories/1
        ///     {
        ///        "CategoryName": "New Category",
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        /// <response code="201">Created.</response>
        /// <response code="204">No content.</response>
        // POST api/<CategoriesController>
        // DELETE api/<CategoriesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, 
            [FromServices] IDeleteCommand command,
            [FromServices] IApplicationUser user)
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
