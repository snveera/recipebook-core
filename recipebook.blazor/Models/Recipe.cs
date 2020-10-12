using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace recipebook.blazor.Models
{
    public class Recipe
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int? Servings { get; set; }

        public int? Rating { get; set; }

        public string Ingredients { get; set; }

        public string Directions { get; set; }

        public string Source { get; set; }

        public string Category { get; set; }
    }
}
