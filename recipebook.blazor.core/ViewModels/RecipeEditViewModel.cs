using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using recipebook.blazor.core.Extensions;
using recipebook.blazor.core.Models;
using recipebook.blazor.core.Services;

namespace recipebook.blazor.core.ViewModels
{
    public class RecipeEditViewModel
    {
        private readonly IRecipeService _recipeService;
        private readonly ICategoryService _categoryService;

        public RecipeEditViewModel(IRecipeService recipeService, ICategoryService categoryService)
        {
            _recipeService = recipeService;
            _categoryService = categoryService;
        }

        public string Id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public int? Servings { get; set; }
        public string Ingredients { get; set; }
        public string Directions { get; set; }
        public string Source { get; set; }
        public int? Rating { get; set; }

        public List<string> Categories { get; private set; } = new List<string>();

        public async Task Initialize(string recipeId)
        {
            await LoadCategories();
            await LoadRecipe(recipeId);
        }

        public async Task Save()
        {

        }

        private async Task LoadCategories()
        {
            var categories = await _categoryService.Get();
            var categoryNames = categories
                .Select(c => c.Name)
                .OrderBy(c => c)
                .ToList();
            this.Categories = categoryNames;
        }

        private async Task LoadRecipe(string recipeId)
        {
            Id = recipeId;
            if (!string.IsNullOrWhiteSpace(recipeId))
            {
                var recipe = await _recipeService.GetById(recipeId) ?? new Recipe {Id = recipeId, Name = "Not Found"};

                Category = recipe.Category;
                Name = recipe.Name;
                Servings = recipe.Servings;
                Ingredients = recipe.Ingredients;
                Directions = recipe.Directions;
                Source = recipe.Source;
            }
        }
    }
}