using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Asp.Application.UseCases.DTO;
using Asp.DataAccess;
using Asp.Implementation.Validation;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Asp.Tests.Validators
{
    public class CategoryValidatorTests
    {
        
        [Fact]
        public void CreateCategoryTests()
        {
           var validator = new CreateCategoryValidator(Context);

            var dto = new CreateCategoryDto {CategoryName = "Spo" };

            var result = validator.Validate(dto);
        }
        
        private AspDbContext Context
        {
            get
            {
                var optionBuilder = new DbContextOptionsBuilder();

                var conString = "Data Source=DESKTOP-LD93OLM\\MSSQLSERVER01;Initial Catalog=AspProjekat;Integrated Security=True";

                optionBuilder.UseSqlServer(conString).UseLazyLoadingProxies();

                var options = optionBuilder.Options;

                return new AspDbContext(options);
            }
        }
    }
}
