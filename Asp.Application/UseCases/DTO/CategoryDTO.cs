using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Application.UseCases.DTO
{
    public class CategoryDTO : CreateCategoryDto
    {
        public int Id { get; set; }
    }

    public class CreateCategoryDto
    {
        public string CategoryName { get; set; }
    }

    public class ModifiedCategoryDto : CategoryDTO
    {
        public string Email { get; set; }
    }
}
