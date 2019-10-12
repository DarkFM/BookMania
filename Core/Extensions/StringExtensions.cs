using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Extensions
{
    public static class StringExtensions
    {
        public static void ThrowIfNullOrWhiteSpace(this string source, string message = default)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException(message ?? "String is null/empty not valid");
        }
    }
}
