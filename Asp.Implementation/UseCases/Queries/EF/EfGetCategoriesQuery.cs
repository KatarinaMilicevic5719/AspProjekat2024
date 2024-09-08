using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;
using Asp.Application.UseCases.DTO.BaseDTOs;
using Asp.Application.UseCases.Queries;
using Asp.DataAccess;

namespace Asp.Implementation.UseCases.Queries.EF
{
    public class EfGetCategoriesQuery : IGetCategoriesQuery
    {
        public int Id => 2;

        public string Name => "Search Categories";

        public string Description => "Search categories using Entity Framework.";


        private AspDbContext _context;
        public EfGetCategoriesQuery(AspDbContext context) => _context = context;


        public IEnumerable<CategoryDTO> Execute(BaseSearch search)
        {
            var query = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.CategoryName.Contains(search.Keyword));
            }

            return query.Select(x => new CategoryDTO
            {
                CategoryName = x.CategoryName,
                Id = x.Id
            }).ToList();
        }
    }
}
