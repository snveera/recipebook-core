using System.Collections.Generic;

namespace recipebook.blazor.Models
{

    public class RecipeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int? Servings { get; set; }

        public List<string> Ingredients { get; set; }

        public List<string> Directions { get; set; }

        public string Source { get; set; }

        public string Category { get; set; }
    }
}
