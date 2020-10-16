using recipebook.blazor.Models;
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

            var lines = value
                .Split(new[] { delimiterResolved }, StringSplitOptions.None)
                .Select(l => l?.Trim())
                .Where(l=>!string.IsNullOrWhiteSpace(l))
                .ToList();

            
            var current = new List<string>();
            var result = new List<List<string>> { current };
            foreach (var line in lines)
            {
                if(line.StartsWith(SpecialCharacters.TitleIndicator))
                {
                    current = new List<string>();
                    result.Add(current);
                }

                current.Add(line);
            }

            return result;
        }
    }
}
