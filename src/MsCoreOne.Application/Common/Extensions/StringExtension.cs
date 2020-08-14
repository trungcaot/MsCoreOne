using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MsCoreOne.Application.Common.Extensions
{
    public static class StringExtension
    {
        public static IList<string> SplitByComma(this string value)
        {
            if (value == null)
            {
                return new List<string>();
            }

            return value.Split(',', StringSplitOptions.None).ToList();
        }

        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length == 1)
            {
                return value;
            }
            var first = value.Substring(0, 1).ToLowerInvariant();
            return first + value.Substring(1);
        }
    }
}
