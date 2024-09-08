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
    public class UserUseCasesController : ControllerBase
    {
        private UseCaseHandler _handler;
        public UserUseCasesController(UseCaseHandler handler)
        {
            _handler = handler;
        }



        /// <summary>
        /// Preko ovog endpoint-a moguce je upravljati UseCase-ovima, svakom izmenom ce se sve
        /// prethodne vrednosti, ako ih ima, izbrisati i postaviti nove
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/UserUseCases/ 
        ///     {
        ///         "UserId" : 1,
        ///         "UseCaseIds" : [1,2,3]
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // PUT api/<UserUseCases>/5
        [HttpPut]
        public IActionResult Put([FromBody] UserUseCasesDto dto,
                                 [FromServices] IModifiedUserUseCasesCommand command)
        {
           
            _handler.HandleCommand(command, dto);
            return Ok();
        }


    }
}
