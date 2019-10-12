using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookMania.Core.Extensions
{
    public static class BaseExtensions
    {
        public static void ThrowIfNull<T>(this T obj, string message = default)
        {
            if (obj == null)
            {
                throw new ArgumentException(message ?? "Argument cannot be null");
            }
        }
    }
}
