using System;
using System.Collections.Generic;
using Asp.DataAccess;
using Asp.DataAccess.Extensions;
using Asp.Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Asp.Tests
{
    public class DbSetExtensionsTests
    { /**
        [Fact]
        public void NonGenericDeactivate_ChangesIsActiveToFalse()
        {
            var category = new Category
            {
                CategoryName = "Test 1",
                IsActive = true,
            };

            var context = new AspDbContext(new TestUser());

            context.Entry(category).State.Should().Be(EntityState.Detached);

            context.Deactivate(category);

            context.Entry(category).State.Should().Be(EntityState.Modified);
            category.IsActive.Should().BeFalse();
        }

        [Fact]
        public void GenericDeactivateThrown_WhenEntityDoesntExist()
        {
            var context = new AspDbContext(new TestUser());

            Action a = () => context.Deactivated<Category>(-500);

            a.Should().ThrowExactly<EntityNotFoundExcception>();

            
        }
    }
        **/
    public class TestUser : IApplicationUser
    {
        public string Identity => "Hardcore user";

            public string Id => throw new NotImplementedException();

            public IEnumerable<int> UseCaseIds => throw new NotImplementedException();

            public string Email => throw new NotImplementedException();

            int IApplicationUser.Id => throw new NotImplementedException();
        }
} }
