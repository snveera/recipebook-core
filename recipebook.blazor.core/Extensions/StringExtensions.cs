using System;
using System.Collections.Generic;
using System.Linq;

namespace recipebook.blazorclient.Application.Extensions
{
    public static class StringExtensions
    {
        public static List<string> ToLineList(this string value, string delimiter = null)
        {
            if(string.IsNullOrWhiteSpace(value))
                return new List<string>();

            var delimiterResolved = delimiter ?? Environment.NewLine;
            return value
                .Split(new []{ delimiterResolved },StringSplitOptions.None)
                .Select(l=>l?.Trim())
                .ToList();
        }
    }
}