using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Extensions
{
    public static class ListExtensions
    {
        public static void ThrowIfNullOrEmpty<T>(this IEnumerable<T> list, string message = default)
        {
            if (!list.Any() || list == null)
            {
                throw new ArgumentException(message ?? "List cannot be null or empty");
            }
        }
    }
}
