using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using recipebook.blazorclient.Application.Services;

namespace recipebook.blazorclient.Application.ViewModels
{
    public class RecipeIndexViewModel
    {
        private readonly CategoryService _categoryService;
        private readonly RecipeService _recipeService;

        public RecipeIndexViewModel(CategoryService categoryService, RecipeService recipeService)
        {
            _categoryService = categoryService;
            _recipeService = recipeService;
        }
        public List<string> Categories { get; set; } = new List<string>();
        public List<RecipeViewModel> Recipes { get; set; } = new List<RecipeViewModel>();

        public string CategoryStatus { 
            get
            {
                return Categories.Any() ? "" : "disabled";
            } 
        }

        public string SearchTerms { get; set; } = "";

        public async Task Search()
        {
            await LoadRecipes(this.SearchTerms);
        }

        public async Task Initialize()
        {
            await LoadCategories();
            await LoadRecipes();
        }

        private async Task LoadCategories()
        {
            var categories = await _categoryService.Get();
            this.Categories = categories
                .Select(c => c.Name)
                .OrderBy(c=>c)
                .ToList();
        }

        private async Task LoadRecipes(string searchTerms = "")
        {
            var data = await _recipeService.Get();
            var viewModels = data
                .Select(Map)
                .OrderBy(r=>r.Name)
                .ToList();
            this.Recipes = viewModels;
        }

        private static RecipeViewModel Map(Models.Recipe toMap)
        {
            return new RecipeViewModel
            {
                Id = toMap.Id,
                Name = toMap.Name,
                Category = toMap.Category,
                Servings = toMap.Servings,
                Ingredients = toMap.Ingredients,
                Directions = toMap.Directions,
                Rating = toMap.Rating,
                Source = toMap.Source
            };
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