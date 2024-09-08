using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.Queries;
using Asp.DataAccess;
using Asp.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asp.Implementation.UseCases.Queries.EF
{
    public class EfGetAuditLogQuery : EfUseCase,IGetAuditLogQuery
    {
        public EfGetAuditLogQuery(AspDbContext context) : base(context)
        {
        }

        public int Id => 24;

        public string Name => "Search Audit Log";

        public string Description => "Search categories using Entity Framework.";

        public PagedResponse<AuditLogDto> Execute(AuditLogSearch search)
        {
            var query = Context.UseCaseLogs.AsQueryable();
            if (!string.IsNullOrEmpty(search.User))
            {
                query = query.Where(x => x.User.ToLower().Contains(search.User.ToLower()));
            }
            if (!string.IsNullOrEmpty(search.UseCaseName))
            {
                query = query.Where(x => x.UseCaseName.ToLower().Contains(search.UseCaseName.ToLower()));
            }
            if (search.DateFrom.HasValue)
            {
                query = query.Where(x => x.ExecutionDateTime >= search.DateFrom);
            }
            if (search.DateTo.HasValue)
            {
                query = query.Where(x => x.ExecutionDateTime <= search.DateTo);
            }
            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.PerPage = 1;
            }
            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<AuditLogDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new AuditLogDto
            {
                UseCaseName=x.UseCaseName,
                User=x.User,
                ExecutionAt=x.ExecutionDateTime,
                Data=x.Data,
            }).ToList();

            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;


            return response;
        }
    }
}
