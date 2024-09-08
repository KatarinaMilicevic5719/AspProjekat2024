using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asp.Domain;

namespace Asp.API.Core
{
    public class JWTUser : IApplicationUser
    {
        public string Identity { get; set; }

        public int Id { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; } = new List<int>();
        public string Email { get; set; }
    }

    public class AnonymousUser : IApplicationUser
    {
        public string Identity => "Anonymous";

        public int Id => 0;

        public IEnumerable<int> UseCaseIds => new List<int> { 1 };

        public string Email => "anonimous@asp-api.com";
    }
}
