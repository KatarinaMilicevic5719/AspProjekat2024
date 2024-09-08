using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.API.Core
{
    public class AppSettings
    {
        public string EmailFrom { get; set; }
        public string EmailPassword { get; set; }
        public string ConString { get; set; }
        public JwtSettings JwtSettings { get; set; }
    }
}
