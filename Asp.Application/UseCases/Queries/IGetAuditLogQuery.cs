using Asp.Application.UseCases.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Application.UseCases.Queries
{
    public interface IGetAuditLogQuery:IQuery<AuditLogSearch,PagedResponse<AuditLogDto>>
    {
    }
}
