using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.API.Core;
using Asp.API.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly JWTMenager _menager;

        public LogInController(JWTMenager menager)
        {
            _menager = menager;
        }

        /// <summary>
        /// Registrovani korisnik moze da se loguje pri cemu se kreira JWT token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/LogIn/
        ///     {
        ///        "Email" : "pera@gmail.com",
        ///        "Password" : Petar123
        ///     }
        ///
        /// </remarks>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Post([FromBody] TokenDto dto)
        {
               var token = _menager.MakeToken(dto.Email, dto.Password);

                return Ok(new { token });
            
        }

        
    }
}
