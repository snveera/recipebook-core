using System.Collections.Generic;
using System.Threading.Tasks;

namespace recipebook.blazorclient.Application.ViewModels
{
    public class RecipeIndexViewModel
    {
        public List<string> Categories { get; set; } = new List<string>();
        public List<RecipeViewModel> Recipes { get; set; } = new List<RecipeViewModel>();

        public string SearchTerms { get; set; } = "";

        public async Task Search()
        {

        }

        public async Task Initialize()
        {
           
        }
    }

    public class RecipeViewModel
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