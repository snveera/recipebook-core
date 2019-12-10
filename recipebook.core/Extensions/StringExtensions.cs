using System;
using System.Globalization;

namespace recipebook.core.Extensions
{
    public static class StringExtensions
    {
        public static bool ContainsCaseInsensitive(this string source, string toCheck)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(toCheck))
                return false;

            return source?.IndexOf(toCheck, StringComparison.CurrentCultureIgnoreCase) >= 0;
        }
    }
}