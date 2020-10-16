using System;
using System.Collections.Generic;
using System.Linq;

namespace recipebook.blazor.Extensions
{
    public static class StringExtensions
    {
        public static List<List<string>> ToLineList(this string value, string delimiter = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                return new List<List<string>>();

            var delimiterResolved = delimiter ?? Environment.NewLine;
            var splitValues = value
                .Split(new[] { delimiterResolved }, StringSplitOptions.None)
                .Select(l => l?.Trim())
                .Where(l=>!string.IsNullOrWhiteSpace(l))
                .ToList();

            return new List<List<string>> { splitValues };
        }
    }
}
