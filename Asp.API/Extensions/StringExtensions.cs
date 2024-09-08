using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.API.Extensions
{
    public static class StringExtensions
    {
        public static bool NotNullOrEmpty(string str)
        {
            return !string.IsNullOrEmpty(str);
        }
    }
}
