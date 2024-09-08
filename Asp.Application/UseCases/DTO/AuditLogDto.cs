using Asp.Application.UseCases.DTO.BaseDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Application.UseCases.DTO
{
    public class AuditLogDto
    {
        public string UseCaseName { get; set; }
        public string User { get; set; }
        public string Data { get; set; }
        public DateTime ExecutionAt { get; set; }
    }
    public class AuditLogSearch:PageSearch
    {
        public string? User { get; set; }
        public string? UseCaseName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set;}
    }
}
