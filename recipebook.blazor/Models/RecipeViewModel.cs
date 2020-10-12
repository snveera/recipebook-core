using recipebook.blazor.Extensions;
using System.Collections.Generic;

namespace recipebook.blazor.Models
{

    public class RecipeViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int? Servings { get; set; }

        public string IngredientsRaw { get; set; }
        public List<string> Ingredients => IngredientsRaw.ToLineList();

        public string DirectionsRaw { get; set; }
        public List<string> Directions => DirectionsRaw.ToLineList();

        public string Source { get; set; }

        public string Category { get; set; }
    }
}
