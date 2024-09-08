using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.Application.UseCases.Queries;
using Asp.Domain;
using Asp.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Asp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private UseCaseHandler _handler;
        public AuditLogController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Pretrazi auditLog, pretrazi sve slucajeve koriscenja koji su izvrseni
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <response code="403">Forbidden.</response>
        /// <response code="404">Not found.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        // GET: api/<AuditLogController>
        [Authorize]
        [HttpGet]
        public IActionResult Get([FromQuery] AuditLogSearch search,
                                 [FromServices] IGetAuditLogQuery query)
        {
          
            return Ok(_handler.HandleQuery(query, search));
        }
    }
}
